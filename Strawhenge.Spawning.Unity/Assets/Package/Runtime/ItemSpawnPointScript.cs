using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnPointScript : MonoBehaviour
    {
        [SerializeField] ItemSpawnCollectionScriptableObject _spawnCollection;

        [SerializeField, Tooltip(
             "Optional. Sets the position and orientation of the spawned items (otherwise uses 'this' transform)")]
        Transform _overridePoint;

        [SerializeField] Collider _playerTriggerCollider;

        Transform _point;
        ItemSpawnScript _currentSpawn;
        bool _canSpawn;

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

        void OnTriggerEnter(Collider other)
        {
            if (!_canSpawn)
                return;

            if (other != _playerTriggerCollider)
                return;

            if (_currentSpawn != null)
                return;

            _currentSpawn = TrySpawnItem();
        }

        ItemSpawnScript TrySpawnItem()
        {
            var items = _spawnCollection.Spawns.ToArray();

            if (items.Length == 0)
                return null;

            var prefab = items.Length == 1
                ? items[0]
                : items[Random.Range(0, items.Length)];

            return Instantiate(prefab, _point.position, _point.rotation);
        }
    }
}