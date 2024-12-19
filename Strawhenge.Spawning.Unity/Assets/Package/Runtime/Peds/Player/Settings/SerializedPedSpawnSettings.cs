using System;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds.Settings
{
    [Serializable]
    public class SerializedPedSpawnSettings : IPedSpawnSettings
    {
        [SerializeField, Min(0), Tooltip(
             "The minimum distance a ped must be from the player before despawn is possible.")]
        float _minDespawnDistance = 20;

        [SerializeField, Min(0), Tooltip(
             "The minimum distance a spawn point must be from the player to spawn a ped (when not visible and not spawning from an entrance.)")]
        float _minSpawnDistance = 20;

        [SerializeField, Min(0), Tooltip(
             "The minimum distance a visible spawn point must be from the player to spawn a ped (when not spawning from an entrance.)")]
        float _minSpawnDistanceWhenVisible = 40;

        [SerializeField, Min(0), Tooltip(
             "The minimum distance an entrance spawn point must be from the player to spawn a ped.")]
        float _minSpawnDistanceFromEntrance = 3;

        [SerializeField, Min(0), Tooltip(
             "The maximum distance a spawn point can be from the player to spawn a ped.")]
        float _maxSpawnDistance = 45;

        public float MinDespawnDistance => _minDespawnDistance;

        public float MinSpawnDistance => _minSpawnDistance;

        public float MinSpawnDistanceWhenVisible => _minSpawnDistanceWhenVisible;

        public float MinSpawnDistanceFromEntrance => _minSpawnDistanceFromEntrance;

        public float MaxSpawnDistance => _maxSpawnDistance;
    }
}