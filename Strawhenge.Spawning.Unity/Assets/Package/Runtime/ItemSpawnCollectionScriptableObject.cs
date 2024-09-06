using Strawhenge.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    [CreateAssetMenu(menuName = "Strawhenge/Spawning/Item Spawn Collection")]
    public class ItemSpawnCollectionScriptableObject : ScriptableObject
    {
        [SerializeField] ItemSpawnScript[] _spawns;

        public IReadOnlyList<ItemSpawnScript> GetSpawnPrefabs() =>
            _spawns
                .ExcludeNull()
                .Distinct()
                .ToArray();
    }
}