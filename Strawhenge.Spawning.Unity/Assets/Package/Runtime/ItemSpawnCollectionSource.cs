using FunctionalUtilities;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnCollectionSource : IItemSpawnSource
    {
        readonly ItemSpawnCollectionScriptableObject _spawnCollection;

        public ItemSpawnCollectionSource(ItemSpawnCollectionScriptableObject spawnCollection)
        {
            _spawnCollection = spawnCollection;
        }

        public Maybe<ItemSpawnScript> TryGetSpawn(Transform parent)
        {
            var items = _spawnCollection.Spawns.ToArray();

            if (items.Length == 0)
                return Maybe.None<ItemSpawnScript>();

            var prefab = items.Length == 1
                ? items[0]
                : items[Random.Range(0, items.Length)];

            return Object.Instantiate(prefab, parent);
        }
    }
}