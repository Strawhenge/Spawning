using Strawhenge.Common;
using System;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnPointsContainer
    {
        readonly Dictionary<ItemSpawnCollectionScriptableObject, List<ItemSpawnPointScript>>
            _spawnPointsBySpawnCollection = new();

        public void Add(ItemSpawnCollectionScriptableObject spawnCollection, ItemSpawnPointScript spawnPoint)
        {
            _spawnPointsBySpawnCollection
                .GetOrAddValue(spawnCollection, () => new List<ItemSpawnPointScript>())
                .Add(spawnPoint);
        }

        public IReadOnlyList<ItemSpawnPointScript> Get(ItemSpawnCollectionScriptableObject itemSpawnCollection)
        {
            return _spawnPointsBySpawnCollection.TryGetValue(itemSpawnCollection, out var spawnPoints)
                ? spawnPoints
                : Array.Empty<ItemSpawnPointScript>();
        }

        public void Clear() => _spawnPointsBySpawnCollection.Clear();
    }
}