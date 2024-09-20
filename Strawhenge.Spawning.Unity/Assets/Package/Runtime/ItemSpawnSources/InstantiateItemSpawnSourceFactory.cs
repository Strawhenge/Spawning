using Strawhenge.Common;
using System.Collections.Generic;

namespace Strawhenge.Spawning.Unity
{
    public class InstantiateItemSpawnSourceFactory : IItemSpawnSourceFactory
    {
        readonly Dictionary<
            ItemSpawnCollectionScriptableObject,
            InstantiateItemSpawnSource> _sourcesBySpawnCollection = new();

        public IItemSpawnSource Create(
            ItemSpawnCollectionScriptableObject spawnCollection,
            ItemSpawnPointScript spawnPoint)
        {
            return _sourcesBySpawnCollection
                .GetOrAddValue(spawnCollection, () => new InstantiateItemSpawnSource(spawnCollection));
        }
    }
}