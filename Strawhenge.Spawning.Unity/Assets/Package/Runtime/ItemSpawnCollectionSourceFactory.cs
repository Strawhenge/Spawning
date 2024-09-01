namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnCollectionSourceFactory : IItemSpawnSourceFactory
    {
        public IItemSpawnSource Create(ItemSpawnCollectionScriptableObject itemSpawnCollection)
        {
            return new ItemSpawnCollectionSource(itemSpawnCollection);
        }
    }
}