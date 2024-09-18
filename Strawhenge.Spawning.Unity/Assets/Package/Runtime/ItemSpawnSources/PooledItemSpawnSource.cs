using FunctionalUtilities;
using Strawhenge.Common.Collections;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class PooledItemSpawnSource : IItemSpawnSource
    {
        Maybe<PredicatedCycle<ItemSpawnPool>> _pools = Maybe.None<PredicatedCycle<ItemSpawnPool>>();

        public PooledItemSpawnSource(
            ItemSpawnPoolsContainer itemSpawnPoolsContainer,
            ItemSpawnCollectionScriptableObject itemSpawnCollection)
        {
            itemSpawnPoolsContainer.Loaded += CreatePoolCycle;

            if (itemSpawnPoolsContainer.IsLoaded)
                CreatePoolCycle();

            void CreatePoolCycle()
            {
                itemSpawnPoolsContainer.Loaded -= CreatePoolCycle;

                var pools = itemSpawnPoolsContainer.GetPool(itemSpawnCollection.GetSpawnPrefabs());
                if (pools.Count > 0)
                    _pools = new PredicatedCycle<ItemSpawnPool>(pool => pool.HasAvailableSpawn, pools);
            }
        }

        public Maybe<ItemSpawnScript> TryGetSpawn(Transform parent)
        {
            return _pools.Map(pools => pools
                    .Next()
                    .Map(pool =>
                    {
                        var spawn = pool.TryGet();
                        spawn.Do(spawnScript =>
                        {
                            spawnScript.transform.parent = parent;
                            spawnScript.transform.SetPositionAndRotation(parent.position, parent.rotation);
                        });
                        return spawn;
                    })
                    .Flatten())
                .Flatten();
        }
    }
}