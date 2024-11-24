﻿using FunctionalUtilities;
using Strawhenge.Common.Collections;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class SpawnPointItemSpawnSource : IItemSpawnSource
    {
        readonly CycleList<ItemSpawnPointScript> _spawnPoints = new(
            predicate: spawnPoint => spawnPoint.HasItem && !spawnPoint.IsInPlayerRadius);

        public Maybe<ItemSpawnScript> TryGetSpawn()
        {
            return _spawnPoints
                .Next()
                .Map(spawnPoint => spawnPoint.TakeItem())
                .Flatten()
                .Do(spawnScript => spawnScript.ResetParts());
        }

        internal void AddSpawnPoint(ItemSpawnPointScript spawnPoint) => _spawnPoints.Add(spawnPoint);
    }
}