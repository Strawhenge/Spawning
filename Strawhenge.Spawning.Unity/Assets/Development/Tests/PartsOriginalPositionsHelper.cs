using Strawhenge.Spawning.Unity.Items;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Strawhenge.Spawning.Unity.Tests
{
    public static class PartsOriginalPositionsHelper
    {
        public static void VerifyPartInOriginalRelativePositions(
            ItemSpawnScript spawn,
            (ItemSpawnPartScript part, Vector3 relativePosition, Quaternion relativeRotation)[] partsOriginalPositions)
        {
            foreach (var (part, position, rotation) in partsOriginalPositions)
            {
                var positionOff = Vector3.Distance(GetRelativePartPosition(spawn, part), position);
                Assert.AreApproximatelyEqual(0, positionOff);

                var rotationOff = Quaternion.Angle(GetRelativePartRotation(spawn, part), rotation);
                Assert.AreApproximatelyEqual(0, rotationOff);
            }
        }

        public static (ItemSpawnPartScript part, Vector3 relativePosition, Quaternion relativeRotation)[]
            GetPartRelativePositions(ItemSpawnScript spawn)
        {
            return spawn.Parts
                .Select(part => (x: part, GetRelativePartPosition(spawn, part), GetRelativePartRotation(spawn, part)))
                .ToArray();
        }

        static Vector3 GetRelativePartPosition(ItemSpawnScript spawn, ItemSpawnPartScript part) =>
            spawn.transform.InverseTransformPoint(part.transform.position);

        static Quaternion GetRelativePartRotation(ItemSpawnScript spawn, ItemSpawnPartScript part) =>
            Quaternion.Inverse(spawn.transform.rotation) * part.transform.rotation;
    }
}