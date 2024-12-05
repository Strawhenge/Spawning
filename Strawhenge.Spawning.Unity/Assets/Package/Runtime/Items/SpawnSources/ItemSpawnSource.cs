using FunctionalUtilities;

namespace Strawhenge.Spawning.Unity.Items
{
    public class ItemSpawnSource : IItemSpawnSource
    {
        readonly PooledItemSpawnSource _pooledSource;
        readonly SpawnPointItemSpawnSource _spawnPointSource;

        public ItemSpawnSource(PooledItemSpawnSource pooledSource, SpawnPointItemSpawnSource spawnPointSource)
        {
            _pooledSource = pooledSource;
            _spawnPointSource = spawnPointSource;
        }

        public Maybe<ItemSpawnScript> TryGetSpawn()
        {
            return _pooledSource
                .TryGetSpawn()
                .Map<Maybe<ItemSpawnScript>>(x => x)
                .Reduce(_spawnPointSource.TryGetSpawn);
        }
    }
}