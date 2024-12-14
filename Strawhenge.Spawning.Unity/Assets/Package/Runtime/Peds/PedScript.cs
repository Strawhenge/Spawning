using FunctionalUtilities;
using UnityEngine;
using UnityEngine.Pool;

namespace Strawhenge.Spawning.Unity.Peds
{
    public class PedScript : MonoBehaviour
    {
        Maybe<IObjectPool<PedScript>> _pool = Maybe.None<IObjectPool<PedScript>>();

        public IPedResetter Resetter { private get; set; }

        public IPedPlacement Placer { private get; set; }

        public void SetPool(IObjectPool<PedScript> pool) => _pool = Maybe.Some(pool);

        public void ResetPed() => Resetter.Reset();

        public void PlaceAt(Vector3 position, Quaternion rotation)
        {
            Placer.PlaceAt(position, rotation);
            gameObject.SetActive(true);
        }

        public void Despawn()
        {
            if (_pool.HasSome(out var pool))
                pool.Release(this);
            else
                Destroy(gameObject);
        }
    }
}