using NUnit.Framework;
using Strawhenge.Common;
using System.Linq;
using Object = UnityEngine.Object;

namespace Strawhenge.Spawning.Unity.Tests
{
    public static class SpawnsHelper
    {
        public static void VerifyNumberOfSpawns(int numberOfSpawns)
        {
            var actualSpawns = GetNumberOfSpawns();
            TestContext.WriteLine($"Spawns: {actualSpawns}");
            Assert.AreEqual(numberOfSpawns, actualSpawns);
        }

        public static void VerifyNumberOfInactiveSpawns(int numberOfSpawns)
        {
            var inactiveSpawns = Object.FindObjectsOfType<ItemSpawnScript>(includeInactive: true)
                .Count(x => !x.gameObject.activeSelf);

            TestContext.WriteLine($"Inactive spawns: {inactiveSpawns}");
            Assert.AreEqual(numberOfSpawns, inactiveSpawns);
        }

        public static void VerifyNoSpawns() => Assert.Zero(GetNumberOfSpawns());

        public static int GetNumberOfSpawns() => GetSpawns().Length;

        public static ItemSpawnScript[] GetSpawns() => Object.FindObjectsOfType<ItemSpawnScript>();

        public static void DespawnHalf()
        {
            var spawns = GetSpawns();

            for (int i = 0; i < spawns.Length / 2; i++)
                spawns[i].Parts.ForEach(x => x.Despawn());
        }
    }
}