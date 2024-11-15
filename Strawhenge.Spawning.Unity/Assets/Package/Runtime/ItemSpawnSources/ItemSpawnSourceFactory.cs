namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnSourceFactory : IItemSpawnSourceFactory
    {
        readonly PooledItemSpawnSourceFactory _pooledItemSpawnSourceFactory;
        readonly SpawnPointItemSpawnSourceFactory _spawnPointItemSpawnSourceFactory;

        public ItemSpawnSourceFactory(
            PooledItemSpawnSourceFactory pooledItemSpawnSourceFactory,
            SpawnPointItemSpawnSourceFactory spawnPointItemSpawnSourceFactory)
        {
            _pooledItemSpawnSourceFactory = pooledItemSpawnSourceFactory;
            _spawnPointItemSpawnSourceFactory = spawnPointItemSpawnSourceFactory;
        }

        public IItemSpawnSource Create(ItemSpawnCollectionScriptableObject spawnCollection,
            ItemSpawnPointScript spawnPoint)
        {
            return new CompositeItemSpawnSource(
                _pooledItemSpawnSourceFactory.Create(spawnCollection, spawnPoint),
                _spawnPointItemSpawnSourceFactory.Create(spawnCollection, spawnPoint));
        }
    }
}