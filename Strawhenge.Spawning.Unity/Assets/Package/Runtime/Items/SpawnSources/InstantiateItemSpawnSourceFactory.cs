using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity.Items
{
    public class InstantiateItemSpawnSourceFactory : IItemSpawnSourceFactory
    {
        readonly Dictionary<
            IItemSpawnCollection,
            InstantiateItemSpawnSource> _sourcesBySpawnCollection = new();

        public IItemSpawnSource Create(
            IItemSpawnCollection spawnCollection,
            ItemSpawnPointScript spawnPoint)
        {
            return _sourcesBySpawnCollection
                .GetOrAddValue(spawnCollection, () => new InstantiateItemSpawnSource(spawnCollection));
        }
    }
}