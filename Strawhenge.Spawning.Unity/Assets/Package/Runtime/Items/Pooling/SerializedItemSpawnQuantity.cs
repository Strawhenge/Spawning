using System;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Items
{
    [Serializable]
    class SerializedItemSpawnQuantity : IItemSpawnQuantity
    {
        [SerializeField] ItemSpawnScript _prefab;
        [SerializeField, Min(1)] int _quantity = 1;

        public ItemSpawnScript Prefab => _prefab;

        public int Quantity => _quantity;
    }
}