using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public interface IItemSpawnPointLayers
    {
        /// <summary>
        /// Layers for any object which, if inside the spawn point,
        /// will block the spawn point from spawning anything until
        /// the object is gone.
        /// </summary>
        LayerMask BlockingLayerMask { get; }
    }
}