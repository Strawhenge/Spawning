using Strawhenge.Spawning.Unity.Peds;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests.Peds
{
    public class FakePlayerPedSpawningScript : BasePlayerPedSpawningScript
    {
        [SerializeField] bool _canSpawn;
        [SerializeField] bool _canSpawnInEntrance;
        [SerializeField] bool _canDespawn;
        [SerializeField] bool _isWithinMaxSpawnDistance;

        public void ArrangeCanSpawn(bool value) => _canSpawn = value;

        public void ArrangeCanSpawnInEntrance(bool value) => _canSpawnInEntrance = value;

        public void ArrangeCanDespawn(bool value) => _canDespawn = value;

        public void ArrangeIsWithinMaxSpawnDistance(bool value) => _isWithinMaxSpawnDistance = value;

        public override bool CanSpawn(Vector3 position) => _canSpawn;

        public override bool CanSpawnInEntrance(Vector3 position) => _canSpawnInEntrance;

        public override bool CanDespawn(GameObject gameObject) => _canDespawn;

        public override bool IsInRadius(Vector3 position) => _isWithinMaxSpawnDistance;
    }
}