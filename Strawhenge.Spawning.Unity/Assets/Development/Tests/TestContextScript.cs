using NUnit.Framework;
using Strawhenge.Common;
using System;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests
{
    public class TestContextScript : MonoBehaviour, ILayersAccessor
    {
        [SerializeField] GameObject _player;
        [SerializeField] LayerMask _blockingLayerMask;
        [SerializeField] ItemSpawnPointScript[] _blockedSpawnPoints;
        [SerializeField] GameObject[] _blockingObjects;

        public ItemSpawnPointScript[] BlockedSpawnPoints { get; private set; }

        public ItemSpawnPointScript[] UnblockedSpawnPoints { get; private set; }

        public ItemSpawnPointScript[] AllSpawnPoints { get; private set; }

        public LayerMask BlockingLayerMask => _blockingLayerMask;

        void Awake()
        {
            AllSpawnPoints = FindObjectsOfType<ItemSpawnPointScript>();
            BlockedSpawnPoints = _blockedSpawnPoints.ExcludeNull().ToArray();
            UnblockedSpawnPoints = AllSpawnPoints.Where(x => !BlockedSpawnPoints.Contains(x)).ToArray();

            foreach (var spawnPoint in AllSpawnPoints)
            {
                spawnPoint.LayersAccessor = this;
            }
        }

        public bool IsInvalid()
        {
            var valid = true;

            if (_player == null)
            {
                TestContext.WriteLine("Player not set.");
                valid = false;
            }

            TestContext.WriteLine($"{UnblockedSpawnPoints.Length} spawn points in scene.");
            if (UnblockedSpawnPoints.Length == 0)
                valid = false;

            return !valid;
        }

        public void MovePlayerTo(ItemSpawnPointScript spawnPoint)
        {
            TestContext.WriteLine($"Moving player to spawn point {spawnPoint.name}.");
            _player.transform.position = spawnPoint.transform.position;
        }

        public void UnblockSpawnPoints()
        {
            foreach (var blockingObject in _blockingObjects)
                Destroy(blockingObject);
        }
    }
}