using FunctionalUtilities;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnCollectionSource : IItemSpawnSource
    {
        readonly IReadOnlyList<ItemSpawnScript> _prefabs;

        public ItemSpawnCollectionSource(ItemSpawnCollectionScriptableObject spawnCollection)
        {
            _prefabs = spawnCollection.GetSpawnPrefabs();
        }

        public Maybe<ItemSpawnScript> TryGetSpawn(Transform parent)
        {
            if (_prefabs.Count == 0)
                return Maybe.None<ItemSpawnScript>();

            var prefab = _prefabs.Count == 1
                ? _prefabs[0]
                : _prefabs[Random.Range(0, _prefabs.Count)];

            return Instantiate(prefab, parent);
        }

        static ItemSpawnScript Instantiate(ItemSpawnScript prefab, Transform parent)
        {
            var instance = Object.Instantiate(prefab, parent);

            instance.DespawnStrategy = spawn => Object.Destroy(spawn.gameObject);
            instance.DespawnPartStrategy = part => Object.Destroy(part.gameObject);

            return instance;
        }
    }
}