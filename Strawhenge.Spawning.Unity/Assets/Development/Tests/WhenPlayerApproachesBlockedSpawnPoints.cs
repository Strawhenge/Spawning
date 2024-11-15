using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests
{
    public class WhenPlayerApproachesBlockedSpawnPoints : BaseTest<TestContextScript>
    {
        protected override string SceneName => SceneNames.ItemSpawnPointTests;

        [UnityTest]
        public IEnumerator BlockedSpawnPointsShouldNotSpawn()
        {
            foreach (var spawnPoint in Context.AllSpawnPoints)
            {
                Context.MovePlayerTo(spawnPoint);
                yield return new WaitForFixedUpdate();
            }

            SpawnsHelper.VerifyNumberOfSpawns(
                Context.UnblockedSpawnPoints.Length);
        }
    }
}