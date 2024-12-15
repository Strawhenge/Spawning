using Strawhenge.Common.Unity.Helpers;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds
{
    public class PedSpawningScript : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] GameObject _player;
        [SerializeField] float _despawnDistance = 20;
        [SerializeField] float _spawnDistance = 20;
        [SerializeField] float _spawnDistanceWhenVisible = 40;
        [SerializeField] float _entranceSpawnDistance = 3;

        public SpawnChecker SpawnChecker { private get; set; }

        void Start()
        {
            ComponentRefHelper.EnsureCamera(ref _camera, nameof(_camera), this);

            if (_player == null)
            {
                Debug.LogWarning($"'{_player}' not assigned. Searching by tag.");

                _player = GameObject.FindGameObjectWithTag("Player");
                if (_player == null)
                {
                    Debug.LogError($"'{_player}' not found. Using '{gameObject}' instead.");
                    _player = gameObject;
                }
            }

            SpawnChecker.Camera = _camera;
            SpawnChecker.Player = _player;
            SpawnChecker.DespawnDistance = _despawnDistance;
            SpawnChecker.SpawnDistance = _spawnDistance;
            SpawnChecker.SpawnDistanceWhenVisible = _spawnDistanceWhenVisible;
            SpawnChecker.EntranceSpawnDistance = _entranceSpawnDistance;
        }

        void OnDestroy()
        {
            SpawnChecker.Camera = null;
            SpawnChecker.Player = null;
        }
    }
}