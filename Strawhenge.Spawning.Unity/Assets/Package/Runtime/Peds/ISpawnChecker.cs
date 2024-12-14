using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public interface ISpawnChecker
    {
        bool CanSpawn(Vector3 position);

        bool CanSpawnInEntrance(Vector3 position);

        bool CanDespawn(GameObject gameObject);

        float GetDistanceTo(Vector3 position);
    }
}