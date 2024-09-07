using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests
{
    public class ItemSpawnPoolTestContextScript : BaseTestContextScript
    {
        [SerializeField] ItemSpawnScript _prefab;
        [SerializeField, Min(1)] int _quantity = 3;

        ItemSpawnPool _pool;

        public ItemSpawnScript Prefab => _prefab;

        public override bool IsInvalid()
        {
            return _prefab == null;
        }

        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            _pool
                .TryGet()
                .Do(spawn => spawn.transform.SetPositionAndRotation(transform.position, transform.rotation));
        }

        void Awake()
        {
            _pool = new ItemSpawnPool(_prefab, _quantity);
        }
    }
}