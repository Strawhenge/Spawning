using Strawhenge.Spawning.Unity;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds
{
    public class PedPlacement : IPedPlacement
    {
        readonly GameObject _gameObject;

        public PedPlacement(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public void PlaceAt(Vector3 position, Quaternion rotation)
        {
            _gameObject.transform.SetPositionAndRotation(position, rotation);
        }
    }
}