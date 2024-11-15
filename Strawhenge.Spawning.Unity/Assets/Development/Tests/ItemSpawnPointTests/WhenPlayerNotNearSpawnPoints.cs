using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests.ItemSpawnPointTests
{
    public class WhenPlayerNotNearSpawnPoints : BaseTest<TestContextScript>
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