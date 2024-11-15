﻿using FunctionalUtilities;
using Strawhenge.Common.Collections;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class PooledItemSpawnSource : IItemSpawnSource
    {
        readonly CycleList<ItemSpawnPool> _pools;

        public PooledItemSpawnSource(
            ItemSpawnPoolsContainer itemSpawnPoolsContainer,
            ItemSpawnCollectionScriptableObject itemSpawnCollection)
        {
            _pools = new CycleList<ItemSpawnPool>(predicate: pool => pool.HasAvailableSpawn);

            itemSpawnPoolsContainer.Loaded += OnPoolsLoaded;

            void OnPoolsLoaded()
            {
                itemSpawnPoolsContainer.Loaded -= OnPoolsLoaded;

                _pools.Add(
                    itemSpawnPoolsContainer.GetPools(
                        itemSpawnCollection.GetSpawnPrefabs()));
            }
        }

        public Maybe<ItemSpawnScript> TryGetSpawn(Transform parent)
        {
            return _pools
                .Next()
                .Map(pool => pool.TryGet())
                .Flatten()
                .Do(spawnScript =>
                {
                    spawnScript.transform.parent = parent;
                    spawnScript.transform.SetPositionAndRotation(parent.position, parent.rotation);
                });
        }
    }
}