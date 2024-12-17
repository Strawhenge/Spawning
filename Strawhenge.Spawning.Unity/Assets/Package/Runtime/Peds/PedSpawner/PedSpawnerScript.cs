using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Random;

namespace Strawhenge.Spawning.Unity.Peds.PedSpawner
{
    public class PedSpawnerScript : MonoBehaviour
    {
        [SerializeField] PedScript[] _spawnablePeds;
        [SerializeField] int _maxSpawns;

        [SerializeField, Tooltip("In seconds.")]
        float _updateSpawnPointsInterval;

        [SerializeField, Tooltip("In seconds.")]
        float _spawnInterval;

        [SerializeField, Tooltip("In seconds.")]
        float _despawnInterval;

        [SerializeField, Tooltip("Number of preloaded instances of each spawnable ped.")]
        int _preloadEachPedCount;

        readonly List<PedScript> _peds = new();

        PedSpawnPointScript[] _allSpawnPoints = Array.Empty<PedSpawnPointScript>();
        PedSpawnPointScript[] _availableSpawnPoints = Array.Empty<PedSpawnPointScript>();
        PedPool _pedPool;

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

            InvokeRepeating(nameof(UpdateSpawnPoints), 0, _updateSpawnPointsInterval);
            InvokeRepeating(nameof(ManageSpawns), 0, _spawnInterval);
            InvokeRepeating(nameof(ManageDespawns), 0, _despawnInterval);
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
                .Where(x => SpawnChecker.IsWithinMaxSpawnDistance(x.transform.position))
                .ToArray();
        }
    }
}