using Strawhenge.Common.Unity;
using Strawhenge.Spawning.Unity.Items;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests.Scripts
{
    public class ContextScript : MonoBehaviour, IItemSpawnPointLayers
    {
        [SerializeField] LayerMask _blockingLayerMask;
        [SerializeField] bool _enablePooling = true;

        void Awake()
        {
            var logger = new UnityLogger(gameObject);
            var poolsContainer = new ItemSpawnPoolsContainer(logger);

            IItemSpawnSourceFactory spawnSourceFactory = _enablePooling
                ? new ItemSpawnSourceFactory(poolsContainer)
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

        LayerMask IItemSpawnPointLayers.BlockingLayerMask => _blockingLayerMask;
    }
}