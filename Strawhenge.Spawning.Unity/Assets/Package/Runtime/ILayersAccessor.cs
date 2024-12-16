using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public interface ILayersAccessor
    {
        int PedSpawnTriggersLayer { get; }
        
        LayerMask ItemSpawnBlockingLayerMask { get; }
    }
}