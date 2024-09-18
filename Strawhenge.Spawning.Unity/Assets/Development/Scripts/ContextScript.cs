using Strawhenge.Common.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class ContextScript : MonoBehaviour, ILayersAccessor
    {
        [SerializeField] LayerMask _blockingLayerMask;

        void Awake()
        {
            var logger = new UnityLogger(gameObject);
            var poolsContainer = new ItemSpawnPoolsContainer(logger);
            var spawnSourceFactory = new PooledItemSpawnSourceFactory(poolsContainer);

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