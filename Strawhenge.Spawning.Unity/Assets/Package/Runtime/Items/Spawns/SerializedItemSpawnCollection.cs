using Strawhenge.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Items
{
    [Serializable]
    public class SerializedItemSpawnCollection : IItemSpawnCollection
    {
        [SerializeField] ItemSpawnScript[] _spawns;

        public IReadOnlyList<ItemSpawnScript> GetSpawnPrefabs() =>
            _spawns
                .ExcludeNull()
                .Distinct()
                .ToArray();
    }
}