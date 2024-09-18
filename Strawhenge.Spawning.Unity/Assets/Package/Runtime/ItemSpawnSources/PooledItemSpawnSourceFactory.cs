using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity
{
    public class PooledItemSpawnSourceFactory : IItemSpawnSourceFactory
    {
        readonly ItemSpawnPoolsContainer _poolsContainer;
        readonly ItemSpawnPointsContainer _spawnPointsContainer;
        readonly Dictionary<ItemSpawnCollectionScriptableObject, IItemSpawnSource> _sourcesBySpawnCollection = new();

        public PooledItemSpawnSourceFactory(ItemSpawnPoolsContainer poolsContainer,
            ItemSpawnPointsContainer spawnPointsContainer)
        {
            _poolsContainer = poolsContainer;
            _spawnPointsContainer = spawnPointsContainer;
        }

        public IItemSpawnSource Create(ItemSpawnCollectionScriptableObject itemSpawnCollection)
        {
            return _sourcesBySpawnCollection.GetOrAddValue(
                itemSpawnCollection,
                () => new PooledItemSpawnSource(_poolsContainer, _spawnPointsContainer, itemSpawnCollection));
        }
    }
}