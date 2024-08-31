using NUnit.Framework;
using Strawhenge.Spawning.Unity;
using UnityEngine;

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

        public static void VerifyNoSpawns() => Assert.Zero(GetNumberOfSpawns());

        public static int GetNumberOfSpawns() => Object.FindObjectsOfType<ItemSpawnScript>().Length;
    }
}