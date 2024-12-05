using FunctionalUtilities;
using Strawhenge.Common.Collections;

namespace Strawhenge.Spawning.Unity.Items
{
    public class PooledItemSpawnSource : IItemSpawnSource
    {
        readonly CycleList<ItemSpawnPool> _pools;

        public PooledItemSpawnSource(
            ItemSpawnPoolsContainer itemSpawnPoolsContainer,
            ItemSpawnCollectionScriptableObject itemSpawnCollection)
        {
            _pools = new CycleList<ItemSpawnPool>(predicate: pool => pool.HasAvailableSpawn);

            if (itemSpawnPoolsContainer.IsLoaded)
                AddPools();
            else
                itemSpawnPoolsContainer.Loaded += OnPoolsLoaded;

            void OnPoolsLoaded()
            {
                itemSpawnPoolsContainer.Loaded -= OnPoolsLoaded;
                AddPools();
            }

            void AddPools()
            {
                _pools.Add(
                    itemSpawnPoolsContainer.GetPools(
                        itemSpawnCollection.GetSpawnPrefabs()));
            }
        }

        public Maybe<ItemSpawnScript> TryGetSpawn()
        {
            return _pools
                .Next()
                .Map(pool => pool.TryGet())
                .Flatten();
        }
    }
}