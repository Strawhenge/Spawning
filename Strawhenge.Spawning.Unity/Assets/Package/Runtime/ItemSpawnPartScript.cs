using System;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnPartScript : MonoBehaviour
    {
        [ContextMenu(nameof(Despawn))]
        public void Despawn()
        {
            OnDespawn(this);
        }

        public Action<ItemSpawnPartScript> OnDespawn { private get; set; } = _ => { };
    }
}