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

        [SerializeField] bool _randomizeDirection;
        [SerializeField] PlayerItemSpawnRadiusScript _playerTrigger;

        readonly List<Collider> _blockingColliders = new();
        Maybe<ItemSpawnScript> _currentSpawn = Maybe.None<ItemSpawnScript>();
        Transform _point;
        IItemSpawnSource _spawnSource;

        public ILayersAccessor LayersAccessor { private get; set; }

        public IItemSpawnSourceFactory SpawnSourceFactory { private get; set; }

        public bool IsInPlayerRadius { get; private set; }

        public bool HasItem => _currentSpawn.HasSome();

        public Maybe<ItemSpawnScript> TakeItem()
        {
            try
            {
                return _currentSpawn;
            }
            finally
            {
                OnCurrentDespawned();
            }
        }

        void Awake()
        {
            _point = _overridePoint != null
                ? _overridePoint
                : transform;

            if (_playerTrigger == null)
            {
                Debug.LogWarning($"'{nameof(_playerTrigger)}' not set. Finding in scene.", this);
                _playerTrigger = FindObjectOfType<PlayerItemSpawnRadiusScript>();

                if (_playerTrigger == null)
                    Debug.LogError($"'{nameof(_playerTrigger)}' not found in scene.", this);
            }

            if (_spawnCollection == null)
                Debug.LogError($"'{nameof(_spawnCollection)}' not set.", this);
        }

        void Start()
        {
            _spawnSource = SpawnSourceFactory.Create(_spawnCollection, this);
        }

        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            AssessBlockingColliders();

            if (CannotSpawn())
                return;

            _currentSpawn = _spawnSource.TryGetSpawn();
            _currentSpawn
                .Do(spawn =>
                {
                    SetSpawnPosition(spawn);
                    spawn.Despawned += OnCurrentDespawned;
                });
        }

        void SetSpawnPosition(ItemSpawnScript spawn)
        {
            spawn.transform.SetParent(_point);

            var rotation = _randomizeDirection
                ? Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up)
                : _point.rotation;

            spawn.transform.SetPositionAndRotation(_point.position, rotation);
        }

        void OnCurrentDespawned()
        {
            _currentSpawn.Do(spawn => spawn.Despawned -= OnCurrentDespawned);
            _currentSpawn = Maybe.None<ItemSpawnScript>();
        }

        bool CannotSpawn() => _blockingColliders.Any() || _currentSpawn.HasSome();

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
            if (other.gameObject == _playerTrigger.gameObject)
            {
                IsInPlayerRadius = true;
                Spawn();
                return;
            }

            if (LayersAccessor.BlockingLayerMask.ContainsLayer(other.gameObject.layer))
            {
                if (!_blockingColliders.Contains(other))
                    _blockingColliders.Add(other);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _playerTrigger.gameObject)
            {
                IsInPlayerRadius = false;
                return;
            }

            if (_blockingColliders.Contains(other))
                _blockingColliders.Remove(other);
        }
    }
}