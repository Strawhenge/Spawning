using Strawhenge.Spawning.Unity.Items;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests.ItemSpawnPointTests
{
    public class WhenSpawnResets : BaseTest<TestContextScript>
    {
        protected override string SceneName => SceneNames.ItemSpawnPointTests;

        [UnityTest]
        public IEnumerator PartsShouldReturnToTheirOriginalPosition()
        {
            var spawnPoint = Context.MultiItemSpawnPoints.First();
            Context.DisableSpawnPoints(except: spawnPoint);

            yield return new WaitForEndOfFrame();

            Context.MovePlayerTo(spawnPoint);
            yield return new WaitForFixedUpdate();

            var spawn = Object.FindObjectOfType<ItemSpawnScript>();

            (ItemSpawnPartScript x, Vector3, Quaternion)[] partsOriginalPositions =
                PartsOriginalPositionsHelper.GetPartRelativePositions(spawn);

            spawn.DisplaceParts();
            spawn.MoveSpawn();
            yield return new WaitForFixedUpdate();

            spawn.ResetParts();
            yield return new WaitForFixedUpdate();

            PartsOriginalPositionsHelper.VerifyPartInOriginalRelativePositions(spawn, partsOriginalPositions);
        }
    }
}