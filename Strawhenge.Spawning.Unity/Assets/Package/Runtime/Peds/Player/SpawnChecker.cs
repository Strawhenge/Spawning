using Strawhenge.Common.Unity;
using Strawhenge.Spawning.Unity.Peds.Settings;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds
{
    class SpawnChecker
    {
        readonly Camera _camera;
        readonly GameObject _player;
        readonly IPedSpawnSettings _settings;

        public SpawnChecker(Camera camera, GameObject player, IPedSpawnSettings settings)
        {
            _camera = camera;
            _player = player;
            _settings = settings;
        }

        public bool CanSpawn(Vector3 position)
        {
            var distance = GetDistanceTo(position);

            return CanSee(position)
                ? distance >= _settings.MinSpawnDistanceWhenVisible
                : distance >= _settings.MinSpawnDistance;
        }

        public bool CanSpawnInEntrance(Vector3 position)
        {
            return GetDistanceTo(position) >= _settings.MinSpawnDistanceFromEntrance;
        }

        public bool CanDespawn(GameObject gameObject)
        {
            return !CanSee(gameObject.transform.position) &&
                   GetDistanceTo(gameObject.transform.position) >= _settings.MinDespawnDistance;
        }

        public bool IsInRadius(Vector3 position)
        {
            return GetDistanceTo(position) <= _settings.MaxSpawnDistance;
        }

        float GetDistanceTo(Vector3 position)
        {
            return Vector3.Distance(position, _player.transform.position);
        }

        bool CanSee(Vector3 position)
        {
            return _camera.IsVisible(position);
        }
    }
}