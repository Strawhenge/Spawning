using Strawhenge.Spawning.Unity.Peds;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests.Peds
{
    public class SpawnCheckerFake : ISpawnChecker
    {
        bool _canSpawn;
        bool _canSpawnInEntrance;
        bool _canDespawn;

        public bool ArrangeCanSpawn(bool value) => _canSpawn = value;

        public bool ArrangeCanSpawnInEntrance(bool value) => _canSpawnInEntrance = value;

        public bool ArrangeCanDespawn(bool value) => _canDespawn = value;

        public bool CanSpawn(Vector3 position) => _canSpawn;

        public bool CanSpawnInEntrance(Vector3 position) => _canSpawnInEntrance;

        public bool CanDespawn(GameObject gameObject) => _canDespawn;

        public float GetDistanceTo(Vector3 position) => 0;
    }
}