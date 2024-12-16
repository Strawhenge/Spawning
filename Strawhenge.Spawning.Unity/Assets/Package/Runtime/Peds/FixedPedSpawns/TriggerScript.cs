using System;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds.FixedPedSpawns
{
    public class TriggerScript : MonoBehaviour
    {
        public event Action PlayerEnter;
        public event Action PlayerExit;

        public ILayersAccessor LayerAccessor { private get; set; }

        void Start()
        {
            gameObject.layer = LayerAccessor.PedSpawnTriggersLayer;
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
