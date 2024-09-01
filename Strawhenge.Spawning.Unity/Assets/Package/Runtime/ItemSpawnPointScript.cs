using Strawhenge.Common.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnPointScript : MonoBehaviour
    {
        [SerializeField] ItemSpawnCollectionScriptableObject _spawnCollection;

        [SerializeField, Tooltip(
             "Optional. Sets the position and orientation of the spawned items (otherwise uses 'this' transform).")]
        Transform _overridePoint;

        [SerializeField] Collider _playerTriggerCollider;

        readonly List<Collider> _blockingColliders = new();
        Transform _point;
        ItemSpawnScript _currentSpawn;
        bool _canSpawn;

        public ILayersAccessor LayersAccessor { private get; set; }

        void Awake()
        {
            _point = _overridePoint != null
                ? _overridePoint
                : transform;

            _canSpawn = true;

            if (_playerTriggerCollider == null)
            {
                Debug.LogError($"'{nameof(_playerTriggerCollider)}' not set.", this);
                _canSpawn = false;
            }

            if (_spawnCollection == null)
            {
                Debug.LogError($"'{nameof(_spawnCollection)}' not set.", this);
                _canSpawn = false;
            }
        }

        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            AssessBlockingColliders();

            if (CannotSpawn())
                return;

            TrySpawnItem();
        }

        bool CannotSpawn() => !_canSpawn || _blockingColliders.Any() || _currentSpawn != null;

        void AssessBlockingColliders()
        {
            for (var i = 0; i < _blockingColliders.Count; i++)
            {
                if (_blockingColliders[i] == null)
                    _blockingColliders.RemoveAt(i);
            }
        }

        void TrySpawnItem()
        {
            var items = _spawnCollection.Spawns.ToArray();

            if (items.Length == 0)
                return;

            var prefab = items.Length == 1
                ? items[0]
                : items[Random.Range(0, items.Length)];

            _currentSpawn = Instantiate(prefab, _point);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other == _playerTriggerCollider)
            {
                Spawn();
            }
            else if (LayersAccessor.BlockingLayerMask.ContainsLayer(other.gameObject.layer))
            {
                if (!_blockingColliders.Contains(other))
                    _blockingColliders.Add(other);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other == _playerTriggerCollider)
                return;

            if (_blockingColliders.Contains(other))
                _blockingColliders.Remove(other);
        }
    }
}