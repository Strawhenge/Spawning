using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests.ItemSpawnPointTests
{
    public class WhenPlayerApproachesPreviouslyBlockedSpawnPoints : BaseTest<TestContextScript>
    {
        protected override string SceneName => SceneNames.ItemSpawnPointTests;

        [UnityTest]
        public IEnumerator SpawnPointsShouldSpawn()
        {
            Context.UnblockSpawnPoints();

            foreach (var spawnPoint in Context.AllSpawnPoints)
            {
                Context.MovePlayerTo(spawnPoint);
                yield return new WaitForFixedUpdate();
            }

            SpawnsHelper.VerifyNumberOfSpawns(
                Context.AllSpawnPoints.Length);
        }
    }
}