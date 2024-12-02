using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity.Items
{
    public interface IItemSpawnCollection
    {
        IReadOnlyList<ItemSpawnScript> GetSpawnPrefabs();
    }
}