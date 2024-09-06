using Strawhenge.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnScript : MonoBehaviour
    {
        [SerializeField] ItemSpawnPartScript[] _parts;

        readonly List<(ItemSpawnPartScript part, Vector3 position, Quaternion rotation)> _originalPartPositions = new();
        int _despawnCount;

        public event Action Despawned;

        public IReadOnlyList<ItemSpawnPartScript> Parts { get; private set; }

        public Action<ItemSpawnScript> DespawnStrategy { private get; set; } =
            spawn => Debug.LogError($"{nameof(DespawnStrategy)} not set.", spawn);

        public Action<ItemSpawnPartScript> DespawnPartStrategy
        {
            set => _parts.ForEach(part => part.DespawnStrategy = value);
        }

        [ContextMenu(nameof(ResetParts))]
        public void ResetParts()
        {
            foreach (var (part, position, rotation) in _originalPartPositions)
            {
                if (part != null)
                    part.transform.SetPositionAndRotation(position, rotation);
            }
        }

        void Awake()
        {
            Parts = _parts.ExcludeNull().ToArray();

            if (Parts.Count == 0)
            {
                Debug.LogError("Item spawn has no parts.", this);
                return;
            }

            foreach (var part in Parts)
            {
                part.Despawned += OnPartDespawned;
                _originalPartPositions.Add((part, part.transform.position, part.transform.rotation));
            }
        }

        void OnPartDespawned(ItemSpawnPartScript part)
        {
            _despawnCount++;
            if (_despawnCount >= Parts.Count)
            {
                DespawnStrategy(this);
                Despawned?.Invoke();
            }
        }
    }
}