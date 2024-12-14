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

        GameObject _spawned;
        Coroutine _flagForDespawn;

        public SpawnChecker SpawnChecker { private get; set; }

        internal void Spawn()
        {
            if (_spawned != null)
            {
                UnflagForDespawn();
                return;
            }

            _spawned = Instantiate(_prefab, transform.position, transform.rotation);

            _onSpawnEvents.ForEach(x => x.Invoke(_spawned));
        }

        internal void Despawn()
        {
            if (_spawned == null)
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
                var wait = new WaitForSeconds(5);

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