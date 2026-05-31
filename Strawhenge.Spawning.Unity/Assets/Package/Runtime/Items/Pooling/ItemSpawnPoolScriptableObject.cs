using Strawhenge.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Items
{
    [CreateAssetMenu(menuName = "Strawhenge/Spawning/Item Spawn Pool")]
    public class ItemSpawnPoolScriptableObject : ScriptableObject
    {
        [SerializeField] SerializedItemSpawnQuantity[] _pool;

        public IReadOnlyList<IItemSpawnQuantity> GetPool()
        {
            return _pool
                .Select(entry =>
                {
                    if (ReferenceEquals(entry.Prefab, null))
                    {
                        Debug.LogError("Prefab not set.", this);
                        return null;
                    }

                    if (entry.Quantity <= 0)
                    {
                        Debug.LogError("Quantity must be greater than 0.", this);
                        return null;
                    }

                    return entry;
                })
                .ExcludeNull()
                .ToArray();
        }
    }
}