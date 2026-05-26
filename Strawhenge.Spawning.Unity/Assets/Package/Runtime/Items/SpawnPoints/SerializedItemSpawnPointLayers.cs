using System;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    [Serializable]
    public class SerializedItemSpawnPointLayers : IItemSpawnPointLayers
    {
        [SerializeField] LayerMask _blockingLayerMask;

        public LayerMask BlockingLayerMask => _blockingLayerMask;
    }
}