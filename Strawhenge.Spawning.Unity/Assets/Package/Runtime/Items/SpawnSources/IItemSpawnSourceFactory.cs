namespace Strawhenge.Spawning.Unity.Items
{
    public interface IItemSpawnSourceFactory
    {
        IItemSpawnSource Create(
            ItemSpawnCollectionScriptableObject spawnCollection,
            ItemSpawnPointScript spawnPoint);
    }
}