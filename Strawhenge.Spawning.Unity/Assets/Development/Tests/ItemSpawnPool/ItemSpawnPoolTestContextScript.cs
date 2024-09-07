using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests
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