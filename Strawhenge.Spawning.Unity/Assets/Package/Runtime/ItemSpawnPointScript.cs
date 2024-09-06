using FunctionalUtilities;
using Strawhenge.Common.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        Maybe<ItemSpawnScript> _currentSpawn = Maybe.None<ItemSpawnScript>();
        Transform _point;
        bool _invalidSetup;
        IItemSpawnSource _spawnSource;

        public ILayersAccessor LayersAccessor { private get; set; }

        public IItemSpawnSourceFactory SpawnSourceFactory { private get; set; }

        void Awake()
        {
            _point = _overridePoint != null
                ? _overridePoint
                : transform;

            if (_playerTriggerCollider == null)
            {
                Debug.LogError($"'{nameof(_playerTriggerCollider)}' not set.", this);
                _invalidSetup = true;
            }

            if (_spawnCollection == null)
            {
                Debug.LogError($"'{nameof(_spawnCollection)}' not set.", this);
                _invalidSetup = true;
            }
        }

        void Start()
        {
            _spawnSource = SpawnSourceFactory.Create(_spawnCollection);
        }

        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            AssessBlockingColliders();

            if (CannotSpawn())
                return;

            _currentSpawn = _spawnSource.TryGetSpawn(_point);
            _currentSpawn.Do(
                spawn => spawn.Despawned += OnCurrentDespawned);
        }

        void OnCurrentDespawned()
        {
            _currentSpawn.Do(spawn => spawn.Despawned -= OnCurrentDespawned);
            _currentSpawn = Maybe.None<ItemSpawnScript>();
        }

        bool CannotSpawn() => _invalidSetup || _blockingColliders.Any() || _currentSpawn.HasSome();

        void AssessBlockingColliders()
        {
            for (var i = 0; i < _blockingColliders.Count; i++)
            {
                if (_blockingColliders[i] == null)
                    _blockingColliders.RemoveAt(i);
            }
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