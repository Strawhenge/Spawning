using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests.Peds
{
    public class FixedPedsSpawnsTests : BaseTest<FixedPedsSpawnsTestContextScript>
    {
        protected override string SceneName => SceneNames.FixedPedSpawnsTests;

        [Test]
        public void Peds_should_not_spawn_when_player_not_in_trigger()
        {
            Assert.IsEmpty(
                Context.SpawnPoints.Where(x => x.HasSpawned));
        }

        [UnityTest]
        public IEnumerator Peds_should_spawn_when_player_enters_trigger()
        {
            Context.MovePlayerToTrigger();

            yield return new WaitForFixedUpdate();

            Assert.AreEqual(
                Context.SpawnPoints.Length,
                Context.SpawnPoints.Count(x => x.HasSpawned));
        }

        [UnityTest]
        public IEnumerator Peds_should_despawn_when_player_exits_trigger_and_peds_are_despawnable()
        {
            Context.MovePlayerToTrigger();

            yield return new WaitForFixedUpdate();

            Context.SpawnChecker.ArrangeCanDespawn(true);
            Context.MovePlayerFromTrigger();

            yield return new WaitForFixedUpdate();

            var despawnTime = Mathf.Max(
                Context.SpawnPoints.Select(p => p.DespawnCheckInterval).ToArray());

            yield return new WaitForSeconds(despawnTime);
            yield return new WaitForFixedUpdate();

            Assert.IsEmpty(
                Context.SpawnPoints.Where(x => x.HasSpawned));
        }
    }
}