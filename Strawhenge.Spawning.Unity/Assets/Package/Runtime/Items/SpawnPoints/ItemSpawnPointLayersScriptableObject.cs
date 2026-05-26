using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    [CreateAssetMenu(menuName = "Strawhenge/Spawning/Item Spawn Point Layers")]
    public class ItemSpawnPointLayersScriptableObject : ScriptableObject, IItemSpawnPointLayers
    {
        [SerializeField] LayerMask _blockingLayerMask;

        public LayerMask BlockingLayerMask => _blockingLayerMask;
    }
}