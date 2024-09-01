using Strawhenge.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    [CreateAssetMenu(menuName = "Strawhenge/Spawning/Item Spawn Collection")]
    public class ItemSpawnCollectionScriptableObject : ScriptableObject
    {
        [SerializeField] ItemSpawnScript[] _spawns;

        public IEnumerable<ItemSpawnScript> Spawns => _spawns.ExcludeNull();
    }
}