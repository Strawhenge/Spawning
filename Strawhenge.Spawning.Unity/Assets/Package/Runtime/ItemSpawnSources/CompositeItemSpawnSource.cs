﻿using FunctionalUtilities;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class CompositeItemSpawnSource : IItemSpawnSource
    {
        readonly IItemSpawnSource[] _sources;

        public CompositeItemSpawnSource(params IItemSpawnSource[] sources)
        {
            _sources = sources;
        }

        public Maybe<ItemSpawnScript> TryGetSpawn()
        {
            foreach (var source in _sources)
            {
                if (source.TryGetSpawn().HasSome(out var spawn))
                    return spawn;
            }

            return Maybe.None<ItemSpawnScript>();
        }
    }
}