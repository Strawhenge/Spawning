namespace Strawhenge.Spawning.Unity.Peds.Settings
{
    public interface IPedSpawnSettings
    {
        float MinDespawnDistance { get; }

        float MinSpawnDistance { get; }

        float MinSpawnDistanceWhenVisible { get; }

        float MinSpawnDistanceFromEntrance { get; }

        float MaxSpawnDistance { get; }
    }
}