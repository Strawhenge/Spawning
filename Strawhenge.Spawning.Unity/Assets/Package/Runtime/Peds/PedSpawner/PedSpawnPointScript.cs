using Strawhenge.Common.Unity;
using System.Collections;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds.PedSpawner
{
    public class PedSpawnPointScript : MonoBehaviour
    {
        [SerializeField] EventScriptableObject[] _onSpawnEvents;

        [SerializeField, Tooltip("In seconds")]
        float _cooldown;

        [SerializeField, Tooltip("Allows spawning while in camera view")]
        bool _isEntrance;

        Transform _transform;
        bool _isAwaitingCooldown;

        void Awake()
        {
            _transform = transform;
        }

        internal bool CanSpawn(ISpawnChecker spawnChecker)
        {
            if (_isAwaitingCooldown)
                return false;

            return _isEntrance
                ? spawnChecker.CanSpawnInEntrance(transform.position)
                : spawnChecker.CanSpawn(transform.position);
        }

        internal void Spawn(PedScript ped)
        {
            ped.PlaceAt(_transform.position, _transform.rotation);
            
            StartCoroutine(RunEvents());
            IEnumerator RunEvents()
            {
                yield return new WaitForEndOfFrame();

                foreach (var @event in _onSpawnEvents)
                    @event.Invoke(ped.gameObject);
            }

            StartCooldown();
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