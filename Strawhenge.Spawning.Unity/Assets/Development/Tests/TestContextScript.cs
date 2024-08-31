using NUnit.Framework;
using Strawhenge.Spawning.Unity;
using UnityEngine;

public class TestContextScript : MonoBehaviour
{
    [SerializeField] GameObject _player;

    public ItemSpawnPointScript[] SpawnPoints { get; private set; }

    void Awake()
    {
        SpawnPoints = FindObjectsOfType<ItemSpawnPointScript>();
    }

    public bool IsInvalid()
    {
        var valid = true;

        if (_player == null)
        {
            TestContext.WriteLine("Player not set.");
            valid = false;
        }

        TestContext.WriteLine($"{SpawnPoints.Length} spawn points in scene.");
        if (SpawnPoints.Length == 0)
            valid = false;

        return !valid;
    }

    public void MovePlayerTo(ItemSpawnPointScript spawnPoint)
    {
        TestContext.WriteLine($"Moving player to spawn point {spawnPoint.name}.");
        _player.transform.position = spawnPoint.transform.position;
    }
}