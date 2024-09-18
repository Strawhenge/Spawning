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
            var spawnSourceFactory = new InstantiateItemSpawnSourceFactory();
            
            foreach (var spawnPoint in FindObjectsOfType<ItemSpawnPointScript>())
            {
                spawnPoint.LayersAccessor = this;
                spawnPoint.SpawnSourceFactory = spawnSourceFactory;
            }
        }

        LayerMask ILayersAccessor.BlockingLayerMask => _blockingLayerMask;
    }
}