using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class SpawningScript : MonoBehaviour
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
            if (_camera == null)
                _camera = Camera.main;

            if (_player == null)
                _player = GameObject.FindGameObjectWithTag("Player");

            SpawnChecker.Camera = _camera;
            SpawnChecker.Player = _player;
            SpawnChecker.DespawnDistance = _despawnDistance;
            SpawnChecker.SpawnDistance = _spawnDistance;
            SpawnChecker.SpawnDistanceWhenVisible = _spawnDistanceWhenVisible;
            SpawnChecker.EntranceSpawnDistance = _entranceSpawnDistance;
        }
    }
}
