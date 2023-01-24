using Strawhenge.Common.Unity;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class SpawnChecker : ISpawnChecker
    {
        internal Camera Camera { private get; set; }

        internal GameObject Player { private get; set; }

        internal float DespawnDistance { private get; set; }

        internal float SpawnDistance { private get; set; }

        internal float SpawnDistanceWhenVisible { private get; set; }

        internal float EntranceSpawnDistance { private get; set; }

        public bool CanSpawn(Vector3 position)
        {
            var distance = GetDistanceTo(position);

            return CanSee(position)
                ? distance >= SpawnDistanceWhenVisible
                : distance >= SpawnDistance;
        }

        public bool CanSpawnInEntrance(Vector3 position) => GetDistanceTo(position) >= EntranceSpawnDistance;

        public bool CanDespawn(GameObject gameObject) =>
            !CanSee(gameObject.transform.position) &&
            GetDistanceTo(gameObject.transform.position) >= DespawnDistance;

        public float GetDistanceTo(Vector3 position)
        {
            if (Player == null)
                return 0;

            return Vector3.Distance(position, Player.transform.position);
        }

        bool CanSee(Vector3 position)
        {
            if (Camera == null)
                return false;

            return Camera.IsVisible(position);
        }
    }
}