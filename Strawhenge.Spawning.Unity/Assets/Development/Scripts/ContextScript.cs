using Strawhenge.Common.Unity;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class ContextScript : MonoBehaviour, ILayersAccessor
    {
        [SerializeField] LayerMask _blockingLayerMask;
        [SerializeField] bool _enablePooling = true;

        void Awake()
        {
            var logger = new UnityLogger(gameObject);
            var poolsContainer = new ItemSpawnPoolsContainer(logger);

            IItemSpawnSourceFactory spawnSourceFactory = _enablePooling
                ? new ItemSpawnSourceFactory(
                    new PooledItemSpawnSourceFactory(poolsContainer),
                    new SpawnPointItemSpawnSourceFactory())
                : new InstantiateItemSpawnSourceFactory();

            foreach (var spawnPoint in FindObjectsOfType<ItemSpawnPointScript>())
            {
                spawnPoint.LayersAccessor = this;
                spawnPoint.SpawnSourceFactory = spawnSourceFactory;
            }

            foreach (var spawnPoolContainer in FindObjectsOfType<ItemSpawnPoolContainerScript>())
            {
                spawnPoolContainer.Container = poolsContainer;
            }
        }

        LayerMask ILayersAccessor.BlockingLayerMask => _blockingLayerMask;
    }
}