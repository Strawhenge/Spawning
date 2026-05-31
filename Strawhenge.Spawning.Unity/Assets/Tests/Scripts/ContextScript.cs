using Strawhenge.Common.Unity;
using Strawhenge.Spawning.Unity.Items;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests.Scripts
{
    public class ContextScript : MonoBehaviour
    {
        [SerializeField] bool _enablePooling = true;

        void Awake()
        {
            var poolsContainer = FindObjectOfType<ItemSpawnPoolContainerScript>();

            IItemSpawnSourceFactory spawnSourceFactory = _enablePooling
                ? new ItemSpawnSourceFactory(poolsContainer.Container)
                : new InstantiateItemSpawnSourceFactory();

            foreach (var spawnPoint in FindObjectsOfType<ItemSpawnPointScript>())
            {
                spawnPoint.SpawnSourceFactory = spawnSourceFactory;
            }
        }
    }
}