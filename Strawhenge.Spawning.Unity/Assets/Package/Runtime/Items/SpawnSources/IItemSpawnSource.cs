using FunctionalUtilities;

namespace Strawhenge.Spawning.Unity.Items
{
    public interface IItemSpawnSource
    {
        Maybe<ItemSpawnScript> TryGetSpawn();
    }
}