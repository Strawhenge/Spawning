using Strawhenge.Common;
using Strawhenge.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Spawning.Unity
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

        public IReadOnlyList<ItemSpawnPool> GetPool(IReadOnlyList<ItemSpawnScript> prefabs)
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
                _poolsByPrefab.Add(prefab, new ItemSpawnPool(prefab, quantity));

            IsLoaded = true;
            Loaded?.Invoke();
        }
    }
}