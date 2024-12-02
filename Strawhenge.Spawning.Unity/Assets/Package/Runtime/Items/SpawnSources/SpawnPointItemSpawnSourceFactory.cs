using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity.Items
{
    public class SpawnPointItemSpawnSourceFactory : IItemSpawnSourceFactory
    {
        readonly Dictionary<ItemSpawnCollectionScriptableObject, SpawnPointItemSpawnSource> _sourcesBySpawnCollection =
            new();

        public IItemSpawnSource Create(
            ItemSpawnCollectionScriptableObject spawnCollection,
            ItemSpawnPointScript spawnPoint)
        {
            var source = _sourcesBySpawnCollection
                .GetOrAddValue(spawnCollection, () => new SpawnPointItemSpawnSource());

            source.AddSpawnPoint(spawnPoint);

            return source;
        }
    }
}