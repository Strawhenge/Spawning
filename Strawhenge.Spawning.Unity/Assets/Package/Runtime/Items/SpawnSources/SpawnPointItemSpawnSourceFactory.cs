using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity.Items
{
    public class SpawnPointItemSpawnSourceFactory : IItemSpawnSourceFactory
    {
        readonly Dictionary<IItemSpawnCollection, SpawnPointItemSpawnSource> _sourcesBySpawnCollection =
            new();

        public IItemSpawnSource Create(
            IItemSpawnCollection spawnCollection,
            ItemSpawnPointScript spawnPoint)
        {
            var source = _sourcesBySpawnCollection
                .GetOrAddValue(spawnCollection, () => new SpawnPointItemSpawnSource());

            source.AddSpawnPoint(spawnPoint);

            return source;
        }
    }
}