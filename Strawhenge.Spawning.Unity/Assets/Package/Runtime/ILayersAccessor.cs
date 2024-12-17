using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public interface ILayersAccessor
    {
        LayerMask ItemSpawnBlockingLayerMask { get; }
    }
}