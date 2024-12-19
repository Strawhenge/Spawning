using Strawhenge.Common;
using Strawhenge.Common.Unity.Helpers;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds.FixedPedSpawns
{
    public class FixedSpawnsScript : MonoBehaviour
    {
        [SerializeField] BasePlayerPedSpawningScript _player;

        FixedSpawnPointScript[] _fixedSpawnPoints;

        void Awake()
        {
            ComponentRefHelper.EnsureSceneComponent(ref _player, nameof(_player), this);

            _fixedSpawnPoints = GetComponentsInChildren<FixedSpawnPointScript>();
            _fixedSpawnPoints.ForEach(p => p.SetPlayer(_player));
        }

        [ContextMenu(nameof(Spawn))]
        void Spawn()
        {
            _fixedSpawnPoints
                .ForEach(x => x.Spawn());
        }

        [ContextMenu(nameof(Despawn))]
        void Despawn()
        {
            _fixedSpawnPoints
                .ForEach(x => x.Despawn());
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _player.gameObject)
                Spawn();
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _player.gameObject)
                Despawn();
        }
    }
}