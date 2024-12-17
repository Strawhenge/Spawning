using Strawhenge.Spawning.Unity.Peds;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests.Peds
{
    public class SpawnCheckerFake : ISpawnChecker
    {
        bool _canSpawn;
        bool _canSpawnInEntrance;
        bool _canDespawn;
        bool _isWithinMaxSpawnDistance;

        public void ArrangeCanSpawn(bool value) => _canSpawn = value;

        public void ArrangeCanSpawnInEntrance(bool value) => _canSpawnInEntrance = value;

        public void ArrangeCanDespawn(bool value) => _canDespawn = value;

        public void ArrangeIsWithinMaxSpawnDistance(bool value) => _isWithinMaxSpawnDistance = value;

        public bool CanSpawn(Vector3 position) => _canSpawn;

        public bool CanSpawnInEntrance(Vector3 position) => _canSpawnInEntrance;

        public bool CanDespawn(GameObject gameObject) => _canDespawn;

        public bool IsWithinMaxSpawnDistance(Vector3 position) => _isWithinMaxSpawnDistance;
    }
}