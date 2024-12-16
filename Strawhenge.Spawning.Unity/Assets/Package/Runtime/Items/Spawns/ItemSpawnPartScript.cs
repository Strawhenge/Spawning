using System;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Items
{
    public class ItemSpawnPartScript : MonoBehaviour
    {
        PlayerItemSpawnRadiusScript _playerItemSpawnRadius;

        void Awake()
        {
            _playerItemSpawnRadius = FindObjectOfType<PlayerItemSpawnRadiusScript>();

            if (_playerItemSpawnRadius == null)
                Debug.LogWarning($"'{nameof(PlayerItemSpawnRadiusScript)}' not found in scene.", this);
        }

        public event Action<ItemSpawnPartScript> Despawned;

        [ContextMenu(nameof(Despawn))]
        public void Despawn()
        {
            DespawnStrategy(this);
            Despawned?.Invoke(this);
        }

        public bool IsInPlayerRadius() =>
            !ReferenceEquals(null, _playerItemSpawnRadius) &&
            _playerItemSpawnRadius.IsInRadius(transform);

        public Action<ItemSpawnPartScript> DespawnStrategy { private get; set; } =
            part => Debug.LogError($"{nameof(DespawnStrategy)} not set.", part);
    }
}