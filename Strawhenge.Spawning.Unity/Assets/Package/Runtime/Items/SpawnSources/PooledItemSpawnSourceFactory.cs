using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity.Items
{
    class PooledItemSpawnSourceFactory
    {
        readonly ItemSpawnPoolsContainer _poolsContainer;
        readonly Dictionary<ItemSpawnCollectionScriptableObject, PooledItemSpawnSource> _sourcesBySpawnCollection = new();

        public PooledItemSpawnSourceFactory(ItemSpawnPoolsContainer poolsContainer)
        {
            _poolsContainer = poolsContainer;
        }

        public PooledItemSpawnSource Create(
            ItemSpawnCollectionScriptableObject spawnCollection)
        {
            return _sourcesBySpawnCollection.GetOrAddValue(
                spawnCollection,
                () => new PooledItemSpawnSource(_poolsContainer, spawnCollection));
        }

        public void Reset()
        {
            _poolsContainer.Clear();
            _sourcesBySpawnCollection.Clear();
        }
    }
}