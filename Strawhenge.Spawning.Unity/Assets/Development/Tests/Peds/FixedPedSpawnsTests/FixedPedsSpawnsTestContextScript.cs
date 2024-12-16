using Strawhenge.Spawning.Unity.Peds.FixedPedSpawns;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests.Peds
{
    public class FixedPedsSpawnsTestContextScript : BaseTestContextScript, ILayersAccessor
    {
        [SerializeField] GameObject _player;
        [SerializeField] FixedSpawnsScript _fixedSpawns;
        [SerializeField] LayerMask _triggersLayer;

        Vector3 _originalPlayerPosition;
        
        public override bool IsInvalid()
        {
            if (_fixedSpawns == null)
            {
                Debug.LogError($"'{nameof(_fixedSpawns)}' not set.");
                return true;
            }

            if (_player == null)
            {
                Debug.LogError($"'{nameof(_player)}' not set.");
                return true;
            }

            return false;
        }

        void Awake()
        {
            _fixedSpawns.LayerAccessor = this;

            SpawnPoints = FindObjectsOfType<FixedSpawnPointScript>(includeInactive: true);
            foreach (var spawnPoint in SpawnPoints)
            {
                spawnPoint.SpawnChecker = SpawnChecker;
            }

            _originalPlayerPosition = _player.transform.position;
        }

        public SpawnCheckerFake SpawnChecker { get; } = new SpawnCheckerFake();

        public FixedSpawnPointScript[] SpawnPoints { get; private set; }

        public int PedSpawnTriggersLayer => _triggersLayer;

        public LayerMask ItemSpawnBlockingLayerMask => 0;

        public void MovePlayerToTrigger()
        {
            _player.transform.position = _fixedSpawns.transform.position;
        }

        public void MovePlayerFromTrigger()
        {
            _player.transform.position = _originalPlayerPosition;
        }
    }
}