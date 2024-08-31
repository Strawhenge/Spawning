using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests
{
    public class WhenPlayerReturnsToDespawnedSpawnPoints : BaseTest
    {
        protected override string SceneName => SceneNames.ItemSpawnPointTests;

        [UnityTest]
        public IEnumerator SpawnPointsShouldRespawn()
        {
            foreach (var spawnPoint in Context.SpawnPoints)
            {
                Context.MovePlayerTo(spawnPoint);
                yield return new WaitForFixedUpdate();
            }
           
            SpawnsHelper.DespawnHalf();
            
            foreach (var spawnPoint in Context.SpawnPoints)
            {
                Context.MovePlayerTo(spawnPoint);
                yield return new WaitForFixedUpdate();
            }
            
            SpawnsHelper.VerifyNumberOfSpawns(Context.SpawnPoints.Length);
        }
    }
}
