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
        [SerializeField] bool _enablePooling = true;

        void Awake()
        {
            var logger = new UnityLogger(gameObject);
            var poolsContainer = new ItemSpawnPoolsContainer(logger);
            var spawnPointsContainer = new ItemSpawnPointsContainer();
            
            IItemSpawnSourceFactory spawnSourceFactory = _enablePooling
                ? new PooledItemSpawnSourceFactory(poolsContainer, spawnPointsContainer)
                : new InstantiateItemSpawnSourceFactory();

            foreach (var spawnPoint in FindObjectsOfType<ItemSpawnPointScript>())
            {
                spawnPoint.LayersAccessor = this;
                spawnPoint.SpawnPointsContainer = spawnPointsContainer;
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