using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity.Items
{
    public class PooledItemSpawnSourceFactory : IItemSpawnSourceFactory
    {
        readonly ItemSpawnPoolsContainer _poolsContainer;
        readonly Dictionary<IItemSpawnCollection, IItemSpawnSource> _sourcesBySpawnCollection = new();

        public PooledItemSpawnSourceFactory(ItemSpawnPoolsContainer poolsContainer)
        {
            _poolsContainer = poolsContainer;
        }

        public IItemSpawnSource Create(
            IItemSpawnCollection spawnCollection,
            ItemSpawnPointScript spawnPoint)
        {
            return _sourcesBySpawnCollection.GetOrAddValue(
                spawnCollection,
                () => new PooledItemSpawnSource(_poolsContainer, spawnCollection));
        }
    }
}