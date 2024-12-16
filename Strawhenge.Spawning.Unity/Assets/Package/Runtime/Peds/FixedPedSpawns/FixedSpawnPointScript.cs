using System.Collections;
using UnityEngine;
using Strawhenge.Common;
using Strawhenge.Common.Unity;

namespace Strawhenge.Spawning.Unity.Peds.FixedPedSpawns
{
    public class FixedSpawnPointScript : MonoBehaviour
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] EventScriptableObject[] _onSpawnEvents;

        [SerializeField, Min(0.1f), Tooltip("In seconds.")]
        float _despawnCheckInterval = 5;

        GameObject _spawned;
        Coroutine _flagForDespawn;

        public ISpawnChecker SpawnChecker { private get; set; }

        public bool HasSpawned => _spawned != null;

        public float DespawnCheckInterval => _despawnCheckInterval;

        internal void Spawn()
        {
            if (HasSpawned)
            {
                UnflagForDespawn();
                return;
            }

            _spawned = Instantiate(_prefab, transform.position, transform.rotation);

            _onSpawnEvents.ForEach(x => x.Invoke(_spawned));
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

            Destroy(_spawned);
        }

        void FlagForDespawn()
        {
            _flagForDespawn = StartCoroutine(FlagForDespawnCoroutine());

            IEnumerator FlagForDespawnCoroutine()
            {
                var wait = new WaitForSeconds(_despawnCheckInterval);

                do
                {
                    yield return wait;
                } while (!CanDespawn());

                Destroy(_spawned);
            }
        }

        void UnflagForDespawn()
        {
            if (_flagForDespawn != null)
                StopCoroutine(_flagForDespawn);
        }

        bool CanDespawn() => SpawnChecker.CanDespawn(_spawned);
    }
}