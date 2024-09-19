using FunctionalUtilities;
using Strawhenge.Common.Collections;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class PooledItemSpawnSource : IItemSpawnSource
    {
        readonly ItemSpawnPointsContainer _itemSpawnPointsContainer;
        readonly ItemSpawnCollectionScriptableObject _itemSpawnCollection;
        Maybe<CycleList<ItemSpawnPool>> _pools = Maybe.None<CycleList<ItemSpawnPool>>();

        public PooledItemSpawnSource(
            ItemSpawnPoolsContainer itemSpawnPoolsContainer,
            ItemSpawnPointsContainer itemSpawnPointsContainer,
            ItemSpawnCollectionScriptableObject itemSpawnCollection)
        {
            _itemSpawnPointsContainer = itemSpawnPointsContainer;
            _itemSpawnCollection = itemSpawnCollection;
            itemSpawnPoolsContainer.Loaded += CreatePoolCycle;

            if (itemSpawnPoolsContainer.IsLoaded)
                CreatePoolCycle();

            void CreatePoolCycle()
            {
                itemSpawnPoolsContainer.Loaded -= CreatePoolCycle;

                var pools = itemSpawnPoolsContainer.GetPool(itemSpawnCollection.GetSpawnPrefabs());
                if (pools.Count > 0)
                    _pools = new CycleList<ItemSpawnPool>(pools, pool => pool.HasAvailableSpawn);
            }
        }

        public Maybe<ItemSpawnScript> TryGetSpawn(Transform parent)
        {
            var spawn = _pools.Map(pools => pools
                    .Next()
                    .Map(pool => pool.TryGet())
                    .Flatten())
                .Flatten()
                .Map(Maybe.Some)
                .Reduce(GetFromSpawnPoint);

            spawn.Do(spawnScript =>
            {
                spawnScript.transform.parent = parent;
                spawnScript.transform.SetPositionAndRotation(parent.position, parent.rotation);
                spawnScript.ResetParts();
            });

            return spawn;
        }

        Maybe<ItemSpawnScript> GetFromSpawnPoint()
        {
            var spawnPoint = _itemSpawnPointsContainer
                .Get(_itemSpawnCollection)
                .FirstOrNone(x => x.HasItem && !x.IsInPlayerRadius);

            return spawnPoint
                .Map(x => x.TakeItem())
                .Flatten();
        }
    }
}