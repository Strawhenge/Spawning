using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds
{
    public abstract class BasePlayerPedSpawningScript : MonoBehaviour
    {
        public abstract bool CanSpawn(Vector3 position);

        public abstract bool CanSpawnInEntrance(Vector3 position);

        public abstract bool CanDespawn(GameObject gameObject);

        public abstract bool IsInRadius(Vector3 position);
    }
}