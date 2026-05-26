using FunctionalUtilities;
using Strawhenge.Common.Unity;
using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Common.Unity.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Items
{
    public class ItemSpawnPointScript : MonoBehaviour
    {
        [SerializeField] PlayerItemSpawnRadiusScript _player;
        [SerializeField] ItemSpawnCollectionScriptableObject _spawnCollection;

        [SerializeField, Tooltip(
             "Optional. Sets the position and orientation of the spawned items (otherwise uses 'this' transform).")]
        Transform _overridePoint;

        [SerializeField] bool _randomizeDirection;

        [SerializeField] SerializedSource<
            IItemSpawnPointLayers,
            SerializedItemSpawnPointLayers,
            ItemSpawnPointLayersScriptableObject> _layers;

        readonly List<Collider> _blockingColliders = new();
        Maybe<ItemSpawnScript> _currentSpawn = Maybe.None<ItemSpawnScript>();
        Transform _point;
        IItemSpawnSource _spawnSource;
        LayerMask? _blockingLayer;

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
            ComponentRefHelper.EnsureSceneComponent(ref _player, nameof(_player), this);

            if (_spawnCollection == null)
                Debug.LogError($"'{nameof(_spawnCollection)}' not set.", this);

            _point = _overridePoint != null
                ? _overridePoint
                : transform;

            if (_layers.TryGetValue(out var layers))
                _blockingLayer = layers.BlockingLayerMask;
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
            if (other.gameObject == _player.gameObject)
            {
                IsInPlayerRadius = true;
                Spawn();
                return;
            }

            if (_blockingLayer.HasValue && _blockingLayer.Value.ContainsLayer(other.gameObject.layer))
            {
                if (!_blockingColliders.Contains(other))
                    _blockingColliders.Add(other);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _player.gameObject)
            {
                IsInPlayerRadius = false;
                return;
            }

            if (_blockingColliders.Contains(other))
                _blockingColliders.Remove(other);
        }
    }
}