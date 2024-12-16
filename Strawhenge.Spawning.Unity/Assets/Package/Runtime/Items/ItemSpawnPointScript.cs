using FunctionalUtilities;
using Strawhenge.Common.Unity;
using Strawhenge.Common.Unity.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Spawning.Unity.Items
{
    public class ItemSpawnPointScript : MonoBehaviour
    {
        [SerializeField] ItemSpawnCollectionScriptableObject _spawnCollection;

        [SerializeField, Tooltip(
             "Optional. Sets the position and orientation of the spawned items (otherwise uses 'this' transform).")]
        Transform _overridePoint;

        [SerializeField] bool _randomizeDirection;
        [FormerlySerializedAs("_playerTrigger")] [SerializeField] PlayerItemSpawnRadiusScript _playerItemSpawnTrigger;

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

            ComponentRefHelper.EnsureSceneComponent(ref _playerItemSpawnTrigger, nameof(_playerItemSpawnTrigger), this);

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

        bool CannotSpawn()
        {
            _blockingColliders.RemoveDestroyed();

            return _blockingColliders.Any() || _currentSpawn.HasSome();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _playerItemSpawnTrigger.gameObject)
            {
                IsInPlayerRadius = true;
                Spawn();
                return;
            }

            if (LayersAccessor.ItemSpawnBlockingLayerMask.ContainsLayer(other.gameObject.layer))
            {
                if (!_blockingColliders.Contains(other))
                    _blockingColliders.Add(other);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _playerItemSpawnTrigger.gameObject)
            {
                IsInPlayerRadius = false;
                return;
            }

            if (_blockingColliders.Contains(other))
                _blockingColliders.Remove(other);
        }
    }
}