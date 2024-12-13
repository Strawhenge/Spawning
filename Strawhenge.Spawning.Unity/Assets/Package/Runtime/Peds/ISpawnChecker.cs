﻿using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds
{
    public interface ISpawnChecker
    {
        bool CanSpawn(Vector3 position);

        bool CanSpawnInEntrance(Vector3 position);

        bool CanDespawn(GameObject gameObject);

        bool IsWithinMaxSpawnDistance(Vector3 position);
    }
}