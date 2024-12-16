using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests.Items.ItemSpawnPointTests
{
    public class WhenPlayerReturnsToDespawnedSpawnPoints : BaseTest<TestContextScript>
    {
        protected override string SceneName => SceneNames.ItemSpawnPointTests;

        [UnityTest]
        public IEnumerator SpawnPointsShouldRespawn()
        {
            foreach (var spawnPoint in Context.UnblockedSpawnPoints)
            {
                Context.MovePlayerTo(spawnPoint);
                yield return new WaitForFixedUpdate();
            }
           
            SpawnsHelper.DespawnHalf();
            
            foreach (var spawnPoint in Context.UnblockedSpawnPoints)
            {
                Context.MovePlayerTo(spawnPoint);
                yield return new WaitForFixedUpdate();
            }
            
            SpawnsHelper.VerifyNumberOfSpawns(Context.UnblockedSpawnPoints.Length);
        }
    }
}
