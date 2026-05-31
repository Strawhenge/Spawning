using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity.Items
{
    class SpawnPointItemSpawnSourceFactory
    {
        readonly Dictionary<IItemSpawnCollection, SpawnPointItemSpawnSource> _sourcesBySpawnCollection =
            new();

        public SpawnPointItemSpawnSource Create(
            IItemSpawnCollection spawnCollection,
            ItemSpawnPointScript spawnPoint)
        {
            var source = _sourcesBySpawnCollection
                .GetOrAddValue(spawnCollection, () => new SpawnPointItemSpawnSource());

            source.AddSpawnPoint(spawnPoint);

            return source;
        }

        public void Reset()
        {
            _sourcesBySpawnCollection.Clear();
        }
    }
}