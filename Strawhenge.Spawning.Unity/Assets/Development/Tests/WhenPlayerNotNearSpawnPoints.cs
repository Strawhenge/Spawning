﻿using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests
{
    public class WhenPlayerNotNearSpawnPoints : BaseTest
    {
        protected override string SceneName => SceneNames.ItemSpawnPointTests;

        [UnityTest]
        public IEnumerator NoSpawnPointsShouldSpawn()
        {
            yield return new WaitForFixedUpdate();
            SpawnsHelper.VerifyNoSpawns();
        }
    }
}