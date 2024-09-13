using FunctionalUtilities;
using Strawhenge.Common;
using System;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnPool
    {
        readonly ObjectPool<ItemSpawnScript> _innerPool;
        readonly int _quantity;

        public ItemSpawnPool(ItemSpawnScript prefab, int quantity)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab));
            if (quantity < 1)
                throw new ArgumentException("Quantity cannot be less than 1.", nameof(quantity));

            _innerPool = new ObjectPool<ItemSpawnScript>(
                createFunc: () =>
                {
                    var instance = Object.Instantiate(prefab);

                    instance.DespawnStrategy = spawn => _innerPool!.Release(spawn);
                    instance.DespawnPartStrategy = spawnPart => spawnPart.gameObject.SetActive(false);

                    return instance;
                },
                actionOnGet: spawn =>
                {
                    spawn.gameObject.SetActive(true);
                    spawn.Parts.ForEach(part => part.gameObject.SetActive(true));
                    spawn.ResetParts();
                },
                actionOnRelease: spawn =>
                {
                    spawn.gameObject.SetActive(false);
                    spawn.Parts.ForEach(part => part.gameObject.SetActive(false));
                },
                collectionCheck: true,
                defaultCapacity: quantity,
                maxSize: quantity);

            _quantity = quantity;
        }

        public bool HasAvailableSpawn => _innerPool.CountActive < _quantity;

        public Maybe<ItemSpawnScript> TryGet()
        {
            if (!HasAvailableSpawn)
                return Maybe.None<ItemSpawnScript>();

            return _innerPool.Get();
        }
    }
}