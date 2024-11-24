using System;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Items
{
    public class ItemSpawnPartScript : MonoBehaviour
    {
        PlayerRadiusScript _playerRadius;

        void Awake()
        {
            _playerRadius = FindObjectOfType<PlayerRadiusScript>();

            if (_playerRadius == null)
                Debug.LogWarning($"'{nameof(PlayerRadiusScript)}' not found in scene.", this);
        }

        public event Action<ItemSpawnPartScript> Despawned;

        [ContextMenu(nameof(Despawn))]
        public void Despawn()
        {
            DespawnStrategy(this);
            Despawned?.Invoke(this);
        }

        public bool IsInPlayerRadius() =>
            !ReferenceEquals(null, _playerRadius) &&
            _playerRadius.IsInRadius(transform);

        public Action<ItemSpawnPartScript> DespawnStrategy { private get; set; } =
            part => Debug.LogError($"{nameof(DespawnStrategy)} not set.", part);
    }
}