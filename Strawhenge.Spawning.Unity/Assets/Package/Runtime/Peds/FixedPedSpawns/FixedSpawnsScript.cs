using Strawhenge.Common;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.FixedPedSpawns
{
    public class FixedSpawnsScript : MonoBehaviour
    {
        FixedSpawnPointScript[] _fixedSpawnPoints;

        void Awake()
        {
            _fixedSpawnPoints = GetComponentsInChildren<FixedSpawnPointScript>();

            var trigger = GetComponentInChildren<TriggerScript>();
            trigger.PlayerEnter += OnPlayerEnter;
            trigger.PlayerExit += OnPlayerExit;
        }

        void OnPlayerEnter()
        {
            _fixedSpawnPoints
                .ForEach(x => x.Spawn());
        }

        void OnPlayerExit()
        {
            _fixedSpawnPoints
                .ForEach(x => x.Despawn());
        }
    }
}
