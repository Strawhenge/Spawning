using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests
{
    public class ItemSpawnPointTests : BaseTest
    {
        protected override string SceneName => SceneNames.ItemSpawnPointTests;

        [UnityTest]
        public IEnumerator Items_should_spawn_when_player_is_near_spawn_points()
        {
            SpawnsHelper.VerifyNoSpawns();
            yield return new WaitForFixedUpdate();

            yield return SpawnPointShouldSpawnWhenPlayerMovesNear();
            yield return SpawnPointsShouldNotRespawnWhenPlayerReturns();
        }

        IEnumerator SpawnPointShouldSpawnWhenPlayerMovesNear()
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

        IEnumerator SpawnPointsShouldNotRespawnWhenPlayerReturns()
        {
            var numberOfSpawns = SpawnsHelper.GetNumberOfSpawns();

            foreach (var spawnPoint in Context.SpawnPoints)
            {
                Context.MovePlayerTo(spawnPoint);
                yield return new WaitForFixedUpdate();

                SpawnsHelper.VerifyNumberOfSpawns(numberOfSpawns);
            }
        }
    }
}