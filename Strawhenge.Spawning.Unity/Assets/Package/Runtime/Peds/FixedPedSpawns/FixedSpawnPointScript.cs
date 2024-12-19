using Strawhenge.Common;
using Strawhenge.Common.Unity;
using Strawhenge.Spawning.Unity.Helpers;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds.FixedPedSpawns
{
    public class FixedSpawnPointScript : MonoBehaviour
    {
        [SerializeField] PedScript _prefab;
        [SerializeField] EventScriptableObject[] _onSpawnEvents;

        [SerializeField, Min(0.1f), Tooltip("In seconds.")]
        float _despawnCheckInterval = 5;

        PedScript _spawned;
        Coroutine _flagForDespawn;
        BasePlayerPedSpawningScript _player;

        public bool HasSpawned => _spawned != null;

        public float DespawnCheckInterval => _despawnCheckInterval;

        internal void SetPlayer(BasePlayerPedSpawningScript player) => _player = player;

        internal void Spawn()
        {
            if (HasSpawned)
            {
                UnflagForDespawn();
                return;
            }

            _spawned = Instantiate(_prefab, transform.position, transform.rotation);

            _onSpawnEvents.ForEach(x => x.Invoke(_spawned.gameObject));
        }

        internal void Despawn()
        {
            if (!HasSpawned)
                return;

            if (!CanDespawn())
            {
                FlagForDespawn();
                return;
            }

            Destroy(_spawned.gameObject);
        }

        void FlagForDespawn()
        {
            _flagForDespawn = StartCoroutine(
                CoroutineHelper.DoWhen(() => Destroy(_spawned.gameObject), CanDespawn, _despawnCheckInterval));
        }

        void UnflagForDespawn()
        {
            if (_flagForDespawn != null)
                StopCoroutine(_flagForDespawn);
        }

        bool CanDespawn()
        {
            if (_player == null)
                return true;
            return _player.CanDespawn(_spawned.gameObject);
        }
    }
}