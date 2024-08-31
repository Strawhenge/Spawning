using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests
{
    public class WhenPlayerApproachesSpawnPoints : BaseTest
    {
        protected override string SceneName => SceneNames.ItemSpawnPointTests;

        [UnityTest]
        public IEnumerator SpawnPointsShouldSpawn()
        {
            var expectedNumberOfSpawns = 0;

            foreach (var spawnPoint in Context.SpawnPoints)
            {
                Context.MovePlayerTo(spawnPoint);
                yield return new WaitForFixedUpdate();

                expectedNumberOfSpawns++;
                SpawnsHelper.VerifyNumberOfSpawns(expectedNumberOfSpawns);
            }
        }
    }
}