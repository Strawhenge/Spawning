using Strawhenge.Spawning.Unity.Items;
using UnityEngine;

namespace Development.Scripts
{
    public class ItemSpawnPoolSpawnerScript : MonoBehaviour
    {
        [SerializeField] ItemSpawnScript _prefab;
        [SerializeField, Min(1)] int _quantity = 3;

        ItemSpawnPool _pool;

        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            _pool?
                .TryGet()
                .Do(spawn => spawn.transform.SetPositionAndRotation(transform.position, transform.rotation));
        }

        void Awake()
        {
            _pool = new ItemSpawnPool(_prefab, _quantity);
        }
    }
}