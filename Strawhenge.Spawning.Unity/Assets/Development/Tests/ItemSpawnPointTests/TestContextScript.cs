using NUnit.Framework;
using Strawhenge.Common;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests.ItemSpawnPointTests
{
    public class TestContextScript : BaseTestContextScript, ILayersAccessor
    {
        [SerializeField] GameObject _player;
        [SerializeField] LayerMask _blockingLayerMask;
        [SerializeField] ItemSpawnPointScript[] _blockedSpawnPoints;
        [SerializeField] GameObject[] _blockingObjects;

        [SerializeField, Tooltip("Spawn points that only have create spawns with multiple parts.")]
        ItemSpawnPointScript[] _multiItemSpawnPoints;

        public ItemSpawnPointScript[] BlockedSpawnPoints { get; private set; }

        public ItemSpawnPointScript[] UnblockedSpawnPoints { get; private set; }

        public ItemSpawnPointScript[] AllSpawnPoints { get; private set; }

        public ItemSpawnPointScript[] MultiItemSpawnPoints { get; private set; }

        public LayerMask BlockingLayerMask => _blockingLayerMask;

        void Awake()
        {
            AllSpawnPoints = FindObjectsOfType<ItemSpawnPointScript>();
            BlockedSpawnPoints = _blockedSpawnPoints.ExcludeNull().ToArray();
            UnblockedSpawnPoints = AllSpawnPoints.Where(x => !BlockedSpawnPoints.Contains(x)).ToArray();
            MultiItemSpawnPoints = _multiItemSpawnPoints.ExcludeNull().ToArray();

            var spawnSourceFactory = new InstantiateItemSpawnSourceFactory();

            foreach (var spawnPoint in AllSpawnPoints)
            {
                spawnPoint.LayersAccessor = this;
                spawnPoint.SpawnSourceFactory = spawnSourceFactory;
            }
        }

        public override bool IsInvalid()
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

        public void DisableSpawnPoints(ItemSpawnPointScript except)
        {
            foreach (var spawnPoint in AllSpawnPoints)
            {
                if (spawnPoint != except)
                    Destroy(spawnPoint.gameObject);
            }
        }
    }
}