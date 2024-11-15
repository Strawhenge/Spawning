using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity
{
    public class PooledItemSpawnSourceFactory : IItemSpawnSourceFactory
    {
        readonly ItemSpawnPoolsContainer _poolsContainer;
        readonly Dictionary<ItemSpawnCollectionScriptableObject, IItemSpawnSource> _sourcesBySpawnCollection = new();

        public PooledItemSpawnSourceFactory(ItemSpawnPoolsContainer poolsContainer)
        {
            _poolsContainer = poolsContainer;
        }

        public IItemSpawnSource Create(
            ItemSpawnCollectionScriptableObject spawnCollection,
            ItemSpawnPointScript spawnPoint)
        {
            return _sourcesBySpawnCollection.GetOrAddValue(
                spawnCollection,
                () => new PooledItemSpawnSource(_poolsContainer, spawnCollection));
        }
    }
}