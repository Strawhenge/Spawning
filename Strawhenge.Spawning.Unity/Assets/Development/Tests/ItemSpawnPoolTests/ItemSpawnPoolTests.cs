using NUnit.Framework;
using Strawhenge.Common;
using Strawhenge.Spawning.Unity.Items;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests.ItemSpawnPoolTests
{
    public class ItemSpawnPoolTests : BaseTest<ItemSpawnPoolTestContextScript>
    {
        protected override string SceneName => SceneNames.ItemSpawnPoolTests;

        [TearDown]
        public void RemoveAllSpawnsFromScene()
        {
            foreach (var spawn in Object.FindObjectsOfType<ItemSpawnScript>())
            {
                spawn.Parts.ForEach(part => Object.Destroy(part.gameObject));
                Object.Destroy(spawn.gameObject);
            }
        }

        [Test]
        public void ShouldReturnInstancesWithinExpectedQuantity()
        {
            const int quantity = 2;
            var pool = new ItemSpawnPool(Context.Prefab, quantity);

            for (int i = 0; i < quantity; i++)
                Assert.IsTrue(pool.TryGet().HasSome());

            Assert.IsFalse(pool.TryGet().HasSome());
        }

        [UnityTest]
        public IEnumerator ShouldCreateExpectedSpawnsInScene()
        {
            const int quantity = 2;
            var pool = new ItemSpawnPool(Context.Prefab, quantity);

            for (int i = 0; i < quantity; i++)
                pool.TryGet();

            yield return new WaitForFixedUpdate();
            SpawnsHelper.VerifyNumberOfSpawns(quantity);

            pool.TryGet();

            yield return new WaitForFixedUpdate();
            SpawnsHelper.VerifyNumberOfSpawns(quantity);
        }

        [UnityTest]
        public IEnumerator SpawnShouldBeInactiveWhenAllPartsDespawned()
        {
            var pool = new ItemSpawnPool(Context.Prefab, 2);

            var spawn = (ItemSpawnScript)pool.TryGet();
            yield return new WaitForFixedUpdate();

            foreach (var part in spawn.Parts)
                part.Despawn();
            yield return new WaitForFixedUpdate();

            SpawnsHelper.VerifyNoSpawns();
            SpawnsHelper.VerifyNumberOfInactiveSpawns(1);
        }

        [UnityTest]
        public IEnumerator ShouldReturnInstanceAfterDespawn()
        {
            var pool = new ItemSpawnPool(Context.Prefab, 1);

            var spawn = (ItemSpawnScript)pool.TryGet();
            yield return new WaitForFixedUpdate();

            foreach (var part in spawn.Parts)
                part.Despawn();
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(pool.TryGet().HasSome());
        }

        [UnityTest]
        public IEnumerator ShouldReactivateInSceneWhenSpawningAfterDespawn()
        {
            var pool = new ItemSpawnPool(Context.Prefab, 1);

            var spawn = (ItemSpawnScript)pool.TryGet();
            yield return new WaitForFixedUpdate();

            foreach (var part in spawn.Parts)
                part.Despawn();
            yield return new WaitForFixedUpdate();

            pool.TryGet();
            yield return new WaitForFixedUpdate();

            SpawnsHelper.VerifyNumberOfSpawns(1);
        }

        [UnityTest]
        public IEnumerator ShouldResetPartWhenSpawningAfterDespawn()
        {
            var pool = new ItemSpawnPool(Context.Prefab, 1);

            var spawn = (ItemSpawnScript)pool.TryGet();
            yield return new WaitForFixedUpdate();

            var partsOriginalPositions = PartsOriginalPositionsHelper.GetPartRelativePositions(spawn);

            spawn.DisplaceParts();
            yield return new WaitForFixedUpdate();

            foreach (var part in spawn.Parts)
                part.Despawn();
            yield return new WaitForFixedUpdate();
            
            spawn = (ItemSpawnScript)pool.TryGet();
            yield return new WaitForFixedUpdate();

            PartsOriginalPositionsHelper.VerifyPartInOriginalRelativePositions(spawn, partsOriginalPositions);
        }
    }
}