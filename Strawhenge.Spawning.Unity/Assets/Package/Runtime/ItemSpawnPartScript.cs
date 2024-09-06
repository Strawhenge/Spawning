using System;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnPartScript : MonoBehaviour
    {
        public event Action<ItemSpawnPartScript> Despawned;

        [ContextMenu(nameof(Despawn))]
        public void Despawn()
        {
            DespawnStrategy(this);
            Despawned?.Invoke(this);
        }

        public Action<ItemSpawnPartScript> DespawnStrategy { private get; set; } = _ => { };
    }
}