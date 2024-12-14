using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public interface IPedPlacement
    {
        void PlaceAt(Vector3 position, Quaternion rotation);
    }
}