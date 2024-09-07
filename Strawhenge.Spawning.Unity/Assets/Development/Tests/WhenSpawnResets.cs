using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Assertions;

namespace Strawhenge.Spawning.Unity.Tests
{
    public class WhenSpawnResets : BaseTest
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

            var partsOriginalPositions = spawn.Parts
                .Select(part => (x: part, GetRelativePartPosition(spawn, part), GetRelativePartRotation(spawn, part)))
                .ToArray();

            DisplaceParts(spawn);
            MoveSpawn(spawn);
            yield return new WaitForFixedUpdate();

            spawn.ResetParts();
            yield return new WaitForFixedUpdate();

            VerifyPartsAreInOriginalPositionRelativeToSpawn(partsOriginalPositions, spawn);
        }

        static void VerifyPartsAreInOriginalPositionRelativeToSpawn(
            (ItemSpawnPartScript x, Vector3, Quaternion)[] partsOriginalPositions, ItemSpawnScript spawn)
        {
            foreach (var (part, position, rotation) in partsOriginalPositions)
            {
                var positionOff = Vector3.Distance(GetRelativePartPosition(spawn, part), position);
                Assert.AreApproximatelyEqual(0, positionOff);

                var rotationOff = Quaternion.Angle(GetRelativePartRotation(spawn, part), rotation);
                Assert.AreApproximatelyEqual(0, rotationOff);
            }
        }

        static Vector3 GetRelativePartPosition(ItemSpawnScript spawn, ItemSpawnPartScript part) =>
            spawn.transform.InverseTransformPoint(part.transform.position);

        static Quaternion GetRelativePartRotation(ItemSpawnScript spawn, ItemSpawnPartScript part) =>
            Quaternion.Inverse(spawn.transform.rotation) * part.transform.rotation;

        static void MoveSpawn(ItemSpawnScript spawn)
        {
            spawn.transform.position += new Vector3(1, 0, 9);
            spawn.transform.rotation *= Quaternion.AngleAxis(45, Vector3.up);
        }

        static void DisplaceParts(ItemSpawnScript spawn)
        {
            for (var i = 0; i < spawn.Parts.Count; i++)
            {
                var part = spawn.Parts[i];
                part.transform.position += Vector3.forward * i;
                part.transform.rotation = Quaternion.Euler(part.transform.rotation.eulerAngles + Vector3.forward * i);
            }
        }
    }
}