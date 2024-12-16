using UnityEngine;

namespace Strawhenge.Spawning.Unity.Items
{
    public class ItemSpawnPoolContainerScript : MonoBehaviour
    {
        [SerializeField] ItemSpawnPoolScriptableObject _pool;

        public ItemSpawnPoolsContainer Container { private get; set; }

        void Start()
        {
            if (_pool == null)
            {
                Debug.LogError($"'{nameof(_pool)}' is not assigned.");
                return;
            }

            Container.Load(_pool.GetPool());
        }

        void OnDestroy()
        {
            Container.Clear();
        }
    }
}