using FunctionalUtilities;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public interface IItemSpawnSource
    {
        Maybe<ItemSpawnScript> TryGetSpawn(Transform parent);
    }
}