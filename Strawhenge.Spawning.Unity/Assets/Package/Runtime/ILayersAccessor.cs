using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public interface ILayersAccessor
    {
        int TriggersLayer { get; }
        
        LayerMask ItemSpawnBlockingLayerMask { get; }
    }
}