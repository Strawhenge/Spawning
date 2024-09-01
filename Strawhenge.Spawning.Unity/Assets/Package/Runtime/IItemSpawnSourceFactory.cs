namespace Strawhenge.Spawning.Unity
{
    public interface IItemSpawnSourceFactory
    {
        IItemSpawnSource Create(ItemSpawnCollectionScriptableObject itemSpawnCollection);
    }
}