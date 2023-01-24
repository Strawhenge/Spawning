using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Random;

namespace Strawhenge.Spawning.Unity.PedSpawner
{
    public class PedSpawnerScript : MonoBehaviour
    {
        [SerializeField] int _maxSpawns;
        [SerializeField] float _maxSpawnDistance;

        [SerializeField, Tooltip("In Seconds")]
        float _updateSpawnPointsInterval;

        [SerializeField, Tooltip("In Seconds")]
        float _spawnInterval;

        [SerializeField, Tooltip("In Seconds")]
        float _despawnInterval;

        readonly List<GameObject> _peds = new List<GameObject>();

        PedSpawnPointScript[] _allSpawnPoints = Array.Empty<PedSpawnPointScript>();
        PedSpawnPointScript[] _availableSpawnPoints = Array.Empty<PedSpawnPointScript>();
        Coroutine _updateSpawnPoints;
        Coroutine _manageDespawns;
        Coroutine _manageSpawns;

        public ISpawnChecker SpawnChecker { private get; set; }

        void Awake()
        {
            _allSpawnPoints = FindObjectsOfType<PedSpawnPointScript>();
        }

        void Start()
        {
            _updateSpawnPoints = StartCoroutine(UpdateSpawnPointsCoroutine());
            _manageSpawns = StartCoroutine(ManageSpawnsCoroutine());
            _manageDespawns = StartCoroutine(ManageDespawnsCoroutine());

            IEnumerator UpdateSpawnPointsCoroutine()
            {
                var interval = new WaitForSeconds(_updateSpawnPointsInterval);
                while (true)
                {
                    UpdateSpawnPoints();
                    yield return interval;
                }
            }

            IEnumerator ManageSpawnsCoroutine()
            {
                var interval = new WaitForSeconds(_spawnInterval);
                while (true)
                {
                    ManageSpawns();
                    yield return interval;
                }
            }

            IEnumerator ManageDespawnsCoroutine()
            {
                var interval = new WaitForSeconds(_despawnInterval);
                while (true)
                {
                    ManageDespawns();
                    yield return interval;
                }
            }
        }

        void OnDisable()
        {
            StopCoroutine(_updateSpawnPoints);
            StopCoroutine(_manageSpawns);
            StopCoroutine(_manageDespawns);
        }

        void ManageSpawns()
        {
            if (_peds.Count >= _maxSpawns)
                return;

            var spawnPoints = _availableSpawnPoints
                .Where(x => x.CanSpawn(SpawnChecker))
                .ToArray();

            if (spawnPoints.Length == 0) return;

            var spawnPoint = spawnPoints.Length == 1
                ? spawnPoints[0]
                : spawnPoints[Range(0, spawnPoints.Length)];

            _peds.Add(spawnPoint.SpawnOne());
        }

        void ManageDespawns()
        {
            foreach (var ped in _peds.ToArray())
            {
                if (ShouldDespawn(ped))
                {
                    _peds.Remove(ped);
                    Destroy(ped);
                }
            }
        }

        bool ShouldDespawn(GameObject ped) => SpawnChecker.CanDespawn(ped);

        void UpdateSpawnPoints()
        {
            _availableSpawnPoints = _allSpawnPoints
                .Where(x => SpawnChecker.GetDistanceTo(x.transform.position) <= _maxSpawnDistance)
                .ToArray();
        }
    }
}