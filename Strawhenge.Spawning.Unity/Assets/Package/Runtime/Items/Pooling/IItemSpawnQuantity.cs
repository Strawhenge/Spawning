namespace Strawhenge.Spawning.Unity.Items
{
    public interface IItemSpawnQuantity
    {
        ItemSpawnScript Prefab { get; }
        int Quantity { get; }
    }
}