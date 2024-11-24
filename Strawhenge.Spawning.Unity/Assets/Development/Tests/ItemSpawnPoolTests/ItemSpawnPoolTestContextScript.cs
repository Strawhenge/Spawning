using Strawhenge.Spawning.Unity.Items;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests.ItemSpawnPoolTests
{
    public class ItemSpawnPoolTestContextScript : BaseTestContextScript
    {
        [SerializeField] ItemSpawnScript _prefab;

        public ItemSpawnScript Prefab => _prefab;

        public override bool IsInvalid()
        {
            return _prefab == null;
        }
    }
}