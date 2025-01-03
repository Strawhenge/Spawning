﻿using Strawhenge.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests.Items.ItemSpawnPointTests
{
    public class WhenAllSpawnPartsDespawned : BaseTest<TestContextScript>
    {
        protected override string SceneName => SceneNames.ItemSpawnPointTests;

        [UnityTest]
        public IEnumerator SpawnsShouldDisappear()
        {
            Context.UnblockedSpawnPoints.ForEach(x => x.Spawn());

            var expectedNumberOfSpawns = SpawnsHelper.GetNumberOfSpawns();

            foreach (var spawn in SpawnsHelper.GetSpawns())
            {
                spawn.Parts.ForEach(x => x.Despawn());
                yield return new WaitForFixedUpdate();

                expectedNumberOfSpawns--;
                SpawnsHelper.VerifyNumberOfSpawns(expectedNumberOfSpawns);
            }
        }
    }
}