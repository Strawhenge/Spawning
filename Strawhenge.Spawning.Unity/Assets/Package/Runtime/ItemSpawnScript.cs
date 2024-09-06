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

        int _despawnCount;

        public event Action Despawned;

        public IReadOnlyList<ItemSpawnPartScript> Parts { get; private set; }

        public Action<ItemSpawnScript> DespawnStrategy { private get; set; }

        public Action<ItemSpawnPartScript> DespawnPartStrategy
        {
            set => _parts.ForEach(part => part.DespawnStrategy = value);
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
                part.transform.parent = null;
                part.Despawned += OnPartDespawned;
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