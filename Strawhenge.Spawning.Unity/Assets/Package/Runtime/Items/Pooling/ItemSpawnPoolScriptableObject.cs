using Strawhenge.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Items
{
    [CreateAssetMenu(menuName = "Strawhenge/Spawning/Item Spawn Pool")]
    public class ItemSpawnPoolScriptableObject : ScriptableObject
    {
        [SerializeField] SerializedItemSpawnPoolEntry[] _pool;

        public IReadOnlyList<(ItemSpawnScript prefab, int quantity)> GetPool()
        {
            return _pool
                .Select<SerializedItemSpawnPoolEntry, (ItemSpawnScript, int)?>(entry =>
                {
                    bool isValid = true;

                    if (ReferenceEquals(entry.Prefab, null))
                    {
                        Debug.LogError("Prefab not set.", this);
                        isValid = false;
                    }

                    if (entry.Quantity <= 0)
                    {
                        Debug.LogError("Quantity must be greater than 0.", this);
                        isValid = false;
                    }

                    return isValid ? (entry.Prefab, entry.Quantity) : null;
                })
                .ExcludeNull()
                .ToArray(x => x!.Value);
        }

        [Serializable]
        class SerializedItemSpawnPoolEntry
        {
            [SerializeField] ItemSpawnScript _prefab;
            [SerializeField, Min(1)] int _quantity = 1;

            public ItemSpawnScript Prefab => _prefab;

            public int Quantity => _quantity;
        }
    }
}