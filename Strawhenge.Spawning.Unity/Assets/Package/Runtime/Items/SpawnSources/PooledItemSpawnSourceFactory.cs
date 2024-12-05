using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity.Items
{
    public class PooledItemSpawnSourceFactory
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
    }
}