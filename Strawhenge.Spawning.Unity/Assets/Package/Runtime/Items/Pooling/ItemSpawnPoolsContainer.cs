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
        readonly ILogger _logger;

        public ItemSpawnPoolsContainer(ILogger logger)
        {
            _logger = logger;
        }

        public event Action Loaded;

        public bool IsLoaded { get; private set; }

        public IReadOnlyList<ItemSpawnPool> GetPools(IReadOnlyList<ItemSpawnScript> prefabs)
        {
            if (!IsLoaded)
            {
                _logger.LogError($"'{nameof(ItemSpawnPoolsContainer)}' not loaded.");
                return Array.Empty<ItemSpawnPool>();
            }

            return prefabs
                .Select(prefab => _poolsByPrefab.GetValueOrDefault(prefab))
                .ExcludeNull()
                .ToArray();
        }

        public void Load(IReadOnlyList<(ItemSpawnScript prefab, int quantity)> pool)
        {
            if (IsLoaded)
            {
                _logger.LogError($"'{nameof(ItemSpawnPoolsContainer)}' is already loaded.");
                return;
            }

            foreach ((ItemSpawnScript prefab, int quantity) in pool)
            {
                if (_poolsByPrefab.ContainsKey(prefab))
                {
                    _logger.LogWarning($"Duplicate prefab '{prefab}' in spawn pool.");
                    continue;
                }

                _poolsByPrefab.Add(prefab, new ItemSpawnPool(prefab, quantity));
            }

            IsLoaded = true;
            Loaded?.Invoke();
        }

        public void Clear()
        {
            _logger.LogInformation("Clearing item spawn pools.");
            _poolsByPrefab.Clear();
            IsLoaded = false;
        }
    }
}