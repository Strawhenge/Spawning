using Strawhenge.Common.Unity;
using Strawhenge.Spawning.Unity.Peds.Settings;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.Spawning.Unity.Peds
{
    public class SpawnChecker : ISpawnChecker
    {
        readonly ILogger _logger;

        Camera _camera;
        GameObject _player;
        IPedSpawnSettings _settings;

        public SpawnChecker(ILogger logger)
        {
            _logger = logger;
        }

        public void Setup(Camera camera, GameObject player, IPedSpawnSettings settings)
        {
            _camera = camera;
            _player = player;
            _settings = settings;
        }

        public void Reset()
        {
            _camera = null;
            _player = null;
            _settings = null;
        }

        public bool CanSpawn(Vector3 position)
        {
            if (IsSetupInvalid())
                return false;

            var distance = GetDistanceTo(position);

            return CanSee(position)
                ? distance >= _settings.MinSpawnDistanceWhenVisible
                : distance >= _settings.MinSpawnDistance;
        }

        public bool CanSpawnInEntrance(Vector3 position)
        {
            if (IsSetupInvalid())
                return false;

            return GetDistanceTo(position) >= _settings.MinSpawnDistanceFromEntrance;
        }

        public bool CanDespawn(GameObject gameObject)
        {
            if (IsSetupInvalid())
                return false;

            return !CanSee(gameObject.transform.position) &&
                   GetDistanceTo(gameObject.transform.position) >= _settings.MinDespawnDistance;
        }

        public bool IsWithinMaxSpawnDistance(Vector3 position)
        {
            if (IsSetupInvalid())
                return false;

            return GetDistanceTo(position) <= _settings.MaxSpawnDistance;
        }

        float GetDistanceTo(Vector3 position)
        {
            if (IsSetupInvalid())
                return 0;

            return Vector3.Distance(position, _player.transform.position);
        }

        bool CanSee(Vector3 position)
        {
            if (IsSetupInvalid())
                return false;

            return _camera.IsVisible(position);
        }

        bool IsSetupInvalid()
        {
            if (ReferenceEquals(_camera, null) || ReferenceEquals(_player, null) || _settings == null)
            {
                _logger.LogError($"'{nameof(SpawnChecker)}' setup is invalid.");
                return true;
            }

            return false;
        }
    }
}