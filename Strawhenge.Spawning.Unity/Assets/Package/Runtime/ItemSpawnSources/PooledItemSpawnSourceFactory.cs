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

        public IItemSpawnSource Create(ItemSpawnCollectionScriptableObject itemSpawnCollection)
        {
            return _sourcesBySpawnCollection
                .GetOrAddValue(itemSpawnCollection, () => CreateSpawnSource(itemSpawnCollection));
        }

        IItemSpawnSource CreateSpawnSource(ItemSpawnCollectionScriptableObject itemSpawnCollection)
        {
            return new PooledItemSpawnSource(_poolsContainer, itemSpawnCollection);
        }
    }
}