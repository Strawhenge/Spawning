namespace Strawhenge.Spawning.Unity.Items
{
    public interface IItemSpawnSourceFactory
    {
        IItemSpawnSource Create(
            IItemSpawnCollection spawnCollection,
            ItemSpawnPointScript spawnPoint);
    }
}