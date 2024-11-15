using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests
{
    public class WhenPlayerReturnsToSpawnPoints : BaseTest<TestContextScript>
    {
        protected override string SceneName => SceneNames.ItemSpawnPointTests;

        [UnityTest]
        public IEnumerator SpawnPointsShouldNotSpawnAgain()
        {
            foreach (var spawnPoint in Context.UnblockedSpawnPoints)
            {
                Context.MovePlayerTo(spawnPoint);
                yield return new WaitForFixedUpdate();
            }

            var numberOfSpawns = SpawnsHelper.GetNumberOfSpawns();

            foreach (var spawnPoint in Context.UnblockedSpawnPoints)
            {
                Context.MovePlayerTo(spawnPoint);
                yield return new WaitForFixedUpdate();

                SpawnsHelper.VerifyNumberOfSpawns(numberOfSpawns);
            }
        }
    }
}