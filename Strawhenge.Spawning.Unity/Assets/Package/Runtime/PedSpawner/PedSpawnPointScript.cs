using Strawhenge.Common.Unity;
using System.Collections;
using UnityEngine;
using static UnityEngine.Random;

namespace Strawhenge.Spawning.Unity.PedSpawner
{
    public class PedSpawnPointScript : MonoBehaviour
    {
        [SerializeField] GameObject[] _spawnablePeds;
        [SerializeField] EventScriptableObject[] _onSpawnEvents;
        [SerializeField, Tooltip("In seconds")] float _cooldown;
        [SerializeField, Tooltip("Allows spawning while in camera view")] bool _isEntrance;

        bool _isAwaitingCooldown;

        void OnEnable()
        {
            if (_spawnablePeds.Length == 0)
            {
                Debug.LogError("Spawn point has no spawnable peds.", this);
                enabled = false;
            }
        }

        internal bool CanSpawn(ISpawnChecker spawnChecker)
        {
            if (_isAwaitingCooldown)
                return false;

            return _isEntrance
                ? spawnChecker.CanSpawnInEntrance(transform.position)
                : spawnChecker.CanSpawn(transform.position);
        }

        internal GameObject SpawnOne()
        {
            var ped = Instantiate(
                _spawnablePeds[Range(0, _spawnablePeds.Length)],
                transform.position,
                transform.rotation);

            StartCoroutine(RunEvents());

            IEnumerator RunEvents()
            {
                yield return new WaitForEndOfFrame();

                foreach (var @event in _onSpawnEvents)
                    @event.Invoke(ped);
            }

            StartCooldown();
            return ped;
        }

        void StartCooldown()
        {
            _isAwaitingCooldown = true;

            StartCoroutine(Cooldown());
            IEnumerator Cooldown()
            {
                yield return new WaitForSeconds(_cooldown);
                _isAwaitingCooldown = false;
            }
        }
    }
}
