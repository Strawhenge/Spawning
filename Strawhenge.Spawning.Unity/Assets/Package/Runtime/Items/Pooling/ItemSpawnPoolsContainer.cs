using Strawhenge.Common;
using Strawhenge.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Spawning.Unity.Items
{
    public class ItemSpawnPoolsContainer
    {
        readonly Dictionary<ItemSpawnScript, ItemSpawnPool> _poolsByPrefab = new();

        public ItemSpawnPoolsContainer(IReadOnlyList<IItemSpawnQuantity> pool, ILogger logger)
        {
            foreach (var itemQuantity in pool)
            {
                if (_poolsByPrefab.ContainsKey(itemQuantity.Prefab))
                {
                    logger.LogWarning($"Duplicate prefab '{itemQuantity.Prefab}' in spawn pool.");
                    continue;
                }

                _poolsByPrefab.Add(
                    itemQuantity.Prefab,
                    new ItemSpawnPool(itemQuantity.Prefab, itemQuantity.Quantity));
            }
        }

        public IReadOnlyList<ItemSpawnPool> GetPools(IReadOnlyList<ItemSpawnScript> prefabs)
        {
            return prefabs
                .Select(prefab => _poolsByPrefab.GetValueOrDefault(prefab))
                .ExcludeNull()
                .ToArray();
        }
    }
}