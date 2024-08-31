using Strawhenge.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class ItemSpawnScript : MonoBehaviour
    {
        [SerializeField] ItemSpawnPartScript[] _parts;

        int _despawnCount;

        public IReadOnlyList<ItemSpawnPartScript> Parts { get; private set; }

        void Awake()
        {
            Parts = _parts.ExcludeNull().ToArray();

            if (Parts.Count == 0)
            {
                Debug.LogError("Item spawn has no parts.", this);
                return;
            }

            foreach (var part in Parts)
            {
                part.transform.parent = null;
                part.OnDespawn = OnPartDespawned;
            }
        }

        void OnPartDespawned(ItemSpawnPartScript part)
        {
            Destroy(part.gameObject);

            _despawnCount++;
            if (_despawnCount >= Parts.Count)
                Destroy(gameObject);
        }
    }
}