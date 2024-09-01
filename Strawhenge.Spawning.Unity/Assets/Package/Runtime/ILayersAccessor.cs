using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public interface ILayersAccessor
    {
        LayerMask BlockingLayerMask { get; }
    }
}