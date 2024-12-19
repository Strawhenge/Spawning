using Strawhenge.Common.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Strawhenge.Spawning.Unity.Peds
{
    class PedPool
    {
        readonly Cycle<ObjectPool<PedScript>> _pools;

        internal PedPool(IEnumerable<PedScript> prefabs)
        {
            _pools = new Cycle<ObjectPool<PedScript>>(prefabs.Select(
                prefab =>
                {
                    return new ObjectPool<PedScript>(
                        createFunc: () =>
                        {
                            var ped = Object.Instantiate(prefab);
                            ped.gameObject.SetActive(false);
                            return ped;
                        },
                        actionOnGet: ped => ped.ResetPed(),
                        actionOnRelease: ped => ped.gameObject.SetActive(false),
                        actionOnDestroy: ped => Object.Destroy(ped.gameObject));
                }));
        }

        internal void PreloadEachPed(int count)
        {
            if (count <= 0) return;

            List<PedScript> instantiatedPeds = new List<PedScript>();
            foreach (var pool in _pools.AsEnumerable())
            {
                for (var i = 0; i < count; i++)
                    instantiatedPeds.Add(pool.Get());

                instantiatedPeds.ForEach(ped => pool.Release(ped));
                instantiatedPeds.Clear();
            }
        }

        internal PedScript Get()
        {
            var pool = _pools.Next();
            var ped = pool.Get();
            ped.SetPool(pool);
            return ped;
        }
    }
}