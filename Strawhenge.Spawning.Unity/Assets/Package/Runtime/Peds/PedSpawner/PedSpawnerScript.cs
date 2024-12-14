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
        [SerializeField] PedScript[] _spawnablePeds;
        [SerializeField] int _maxSpawns;
        [SerializeField] float _maxSpawnDistance;

        [SerializeField, Tooltip("In Seconds")]
        float _updateSpawnPointsInterval;

        [SerializeField, Tooltip("In Seconds")]
        float _spawnInterval;

        [SerializeField, Tooltip("In Seconds")]
        float _despawnInterval;

        [SerializeField, Tooltip("Number of preloaded instances of each spawnable ped.")]
        int _preloadEachPedCount;

        readonly List<PedScript> _peds = new List<PedScript>();

        PedSpawnPointScript[] _allSpawnPoints = Array.Empty<PedSpawnPointScript>();
        PedSpawnPointScript[] _availableSpawnPoints = Array.Empty<PedSpawnPointScript>();
        PedPool _pedPool;
        Coroutine _updateSpawnPoints;
        Coroutine _manageDespawns;
        Coroutine _manageSpawns;

        public ISpawnChecker SpawnChecker { private get; set; }

        [ContextMenu(nameof(DespawnAll))]
        public void DespawnAll()
        {
            foreach (var ped in _peds.ToArray())
                Despawn(ped);
        }

        void Awake()
        {
            _allSpawnPoints = FindObjectsOfType<PedSpawnPointScript>();
            _pedPool = new PedPool(_spawnablePeds);
        }

        void Start()
        {
            _pedPool.PreloadEachPed(_preloadEachPedCount);

            _updateSpawnPoints = StartCoroutine(UpdateSpawnPointsCoroutine());
            _manageSpawns = StartCoroutine(ManageSpawnsCoroutine());
            _manageDespawns = StartCoroutine(ManageDespawnsCoroutine());

            IEnumerator UpdateSpawnPointsCoroutine()
            {
                var interval = new WaitForSeconds(_updateSpawnPointsInterval);
                while (enabled)
                {
                    UpdateSpawnPoints();
                    yield return interval;
                }
            }

            IEnumerator ManageSpawnsCoroutine()
            {
                var interval = new WaitForSeconds(_spawnInterval);
                while (enabled)
                {
                    ManageSpawns();
                    yield return interval;
                }
            }

            IEnumerator ManageDespawnsCoroutine()
            {
                var interval = new WaitForSeconds(_despawnInterval);
                while (enabled)
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

            try
            {
                var ped = _pedPool.Get();
                spawnPoint.Spawn(ped);
                _peds.Add(ped);
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }

        void ManageDespawns()
        {
            foreach (var ped in _peds.ToArray())
            {
                if (ShouldDespawn(ped.gameObject))
                {
                    Despawn(ped);
                }
            }
        }

        void Despawn(PedScript ped)
        {
            try
            {
                _peds.Remove(ped);
                ped.Despawn();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
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