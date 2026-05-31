using Strawhenge.Common.Unity;
using System;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Items
{
    public sealed class ItemSpawnPoolContainerScript : MonoBehaviour
    {
        [SerializeField] ItemSpawnPoolScriptableObject _pool;
        [SerializeField] LoggerScript _logger;

        ItemSpawnPoolsContainer _container;

        public ItemSpawnPoolsContainer Container => _container ??= Create();

        void Awake()
        {
            _container ??= Create();
        }

        ItemSpawnPoolsContainer Create()
        {
            var logger = _logger != null
                ? _logger.Logger
                : new UnityLogger(gameObject);

            if (_pool == null)
            {
                logger.LogError($"'{nameof(_pool)}' is not assigned.");
                return new ItemSpawnPoolsContainer(Array.Empty<IItemSpawnQuantity>(), logger);
            }

            return new ItemSpawnPoolsContainer(_pool.GetPool(), logger);
        }
    }
}