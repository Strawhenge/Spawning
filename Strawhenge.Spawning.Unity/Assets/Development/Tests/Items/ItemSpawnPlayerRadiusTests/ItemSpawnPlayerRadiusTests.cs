using NUnit.Framework;

namespace Strawhenge.Spawning.Unity.Tests.Items.ItemSpawnPlayerRadiusTests
{
    public class ItemSpawnPlayerRadiusTests : BaseTest<ItemSpawnPlayerRadiusTestsContextScript>
    {
        protected override string SceneName => SceneNames.ItemSpawnPlayerRadiusTests;

        [Test]
        public void ItemSpawnsInRadius()
        {
            foreach (var spawn in Context.SpawnsInRadius)
            {
                Assert.IsTrue(spawn.IsInPlayerRadius());
            }
        }
        
        [Test]
        public void ItemSpawnsNotInRadius()
        {
            foreach (var spawn in Context.SpawnsNotInRadius)
            {
                Assert.IsFalse(spawn.IsInPlayerRadius());
            }
        }
    }
}
