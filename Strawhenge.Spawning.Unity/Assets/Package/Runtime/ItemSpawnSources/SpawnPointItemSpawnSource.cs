using FunctionalUtilities;
using Strawhenge.Common.Collections;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class SpawnPointItemSpawnSource : IItemSpawnSource
    {
        readonly CycleList<ItemSpawnPointScript> _spawnPoints = new(
            predicate: spawnPoint => spawnPoint.HasItem && !spawnPoint.IsInPlayerRadius);

        public Maybe<ItemSpawnScript> TryGetSpawn(Transform parent)
        {
            var spawn = _spawnPoints
                .Next()
                .Map(spawnPoint => spawnPoint.TakeItem())
                .Flatten();

            spawn.Do(spawnScript =>
            {
                spawnScript.transform.parent = parent;
                spawnScript.transform.SetPositionAndRotation(parent.position, parent.rotation);
                spawnScript.ResetParts();
            });

            return spawn;
        }

        internal void AddSpawnPoint(ItemSpawnPointScript spawnPoint) => _spawnPoints.Add(spawnPoint);
    }
}