using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnCollectionSourceFactory : IItemSpawnSourceFactory
    {
        readonly Dictionary<ItemSpawnCollectionScriptableObject, ItemSpawnCollectionSource> _sourcesBySpawnCollection =
            new();

        public IItemSpawnSource Create(ItemSpawnCollectionScriptableObject itemSpawnCollection)
        {
            return _sourcesBySpawnCollection
                .GetOrAddValue(itemSpawnCollection, () => new ItemSpawnCollectionSource(itemSpawnCollection));
        }
    }
}