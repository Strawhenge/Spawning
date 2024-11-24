using Strawhenge.Spawning.Unity.Items;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests.ItemSpawnPlayerRadiusTests
{
    public class ItemSpawnPlayerRadiusTestsContextScript : BaseTestContextScript
    {
        [SerializeField] ItemSpawnScript[] _spawnsInRadius;
        [SerializeField] ItemSpawnScript[] _spawnsNotInRadius;

        public override bool IsInvalid() =>
            _spawnsInRadius.Length == 0 ||
            _spawnsNotInRadius.Length == 0;

        public ItemSpawnScript[] SpawnsInRadius => _spawnsInRadius;

        public ItemSpawnScript[] SpawnsNotInRadius => _spawnsNotInRadius;
    }
}