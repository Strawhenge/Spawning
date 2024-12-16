using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds
{
    public interface IPedPlacement
    {
        void PlaceAt(Vector3 position, Quaternion rotation);
    }
}