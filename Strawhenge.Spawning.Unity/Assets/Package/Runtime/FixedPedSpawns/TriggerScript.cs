using System;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.FixedPedSpawns
{
    public class TriggerScript : MonoBehaviour
    {
        public event Action PlayerEnter;
        public event Action PlayerExit;

        public ITriggersLayerAccessor TriggersLayerAccessor { private get; set; }

        void Start()
        {
            gameObject.layer = TriggersLayerAccessor.Layer;
        }

        void OnTriggerEnter(Collider other)
        {
            PlayerEnter?.Invoke();
        }

        void OnTriggerExit(Collider other)
        {
            PlayerExit?.Invoke();
        }
    }
}
