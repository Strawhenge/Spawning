using FunctionalUtilities;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Items
{
    public class InstantiateItemSpawnSource : IItemSpawnSource
    {
        readonly IReadOnlyList<ItemSpawnScript> _prefabs;

        public InstantiateItemSpawnSource(IItemSpawnCollection spawnCollection)
        {
            _prefabs = spawnCollection.GetSpawnPrefabs();
        }

        public Maybe<ItemSpawnScript> TryGetSpawn()
        {
            if (_prefabs.Count == 0)
                return Maybe.None<ItemSpawnScript>();

            var prefab = _prefabs.Count == 1
                ? _prefabs[0]
                : _prefabs[Random.Range(0, _prefabs.Count)];

            return Instantiate(prefab);
        }

        static ItemSpawnScript Instantiate(ItemSpawnScript prefab)
        {
            var instance = Object.Instantiate(prefab);

            instance.DespawnStrategy = spawn => Object.Destroy(spawn.gameObject);
            instance.DespawnPartStrategy = part => Object.Destroy(part.gameObject);

            return instance;
        }
    }
}