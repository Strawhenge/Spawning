namespace Strawhenge.Spawning.Unity.Items
{
    public class ItemSpawnSourceFactory : IItemSpawnSourceFactory
    {
        readonly PooledItemSpawnSourceFactory _pooledItemSpawnSourceFactory;
        readonly SpawnPointItemSpawnSourceFactory _spawnPointItemSpawnSourceFactory;

        public ItemSpawnSourceFactory(ItemSpawnPoolsContainer spawnPoolsContainer)
        {
            _pooledItemSpawnSourceFactory = new PooledItemSpawnSourceFactory(spawnPoolsContainer);
            _spawnPointItemSpawnSourceFactory = new SpawnPointItemSpawnSourceFactory();
        }

        public IItemSpawnSource Create(
            ItemSpawnCollectionScriptableObject spawnCollection,
            ItemSpawnPointScript spawnPoint)
        {
            return new ItemSpawnSource(
                _pooledItemSpawnSourceFactory.Create(spawnCollection),
                _spawnPointItemSpawnSourceFactory.Create(spawnCollection, spawnPoint));
        }

        public void Reset()
        {
            _pooledItemSpawnSourceFactory.Reset();
            _spawnPointItemSpawnSourceFactory.Reset();
        }
    }
}