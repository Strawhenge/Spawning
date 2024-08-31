using NUnit.Framework;
using Strawhenge.Spawning.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ItemSpawnPointTests
{
    const string Scene = "ItemSpawnPointTests";
    ItemSpawnPointTestsScript _context;

    [UnitySetUp]
    public IEnumerator LoadScene()
    {
        yield return SceneManager.LoadSceneAsync(Scene);
        _context = Object.FindObjectOfType<ItemSpawnPointTestsScript>();

        if (_context == null || _context.IsInvalid())
            Assert.Fail("Test context is invalid.");
    }
   
    [UnityTest]
    public IEnumerator Items_should_spawn_when_player_is_near_spawn_points()
    {
        VerifyNoSpawns();
        yield return new WaitForFixedUpdate();

        yield return SpawnPointShouldSpawnWhenPlayerMovesNear();
        yield return SpawnPointsShouldNotRespawnWhenPlayerReturns();
    }

    IEnumerator SpawnPointShouldSpawnWhenPlayerMovesNear()
    {
        int expectedNumberOfSpawns = 0;

        foreach (var spawnPoint in _context.SpawnPoints)
        {
            MovePlayerTo(spawnPoint);
            yield return new WaitForFixedUpdate();
            
            expectedNumberOfSpawns++;
            VerifyNumberOfSpawns(expectedNumberOfSpawns);
        }
    }
    
    IEnumerator SpawnPointsShouldNotRespawnWhenPlayerReturns()
    {
        var numberOfSpawns = GetNumberOfSpawns();
        
        foreach (var spawnPoint in _context.SpawnPoints)
        {
            MovePlayerTo(spawnPoint);
            yield return new WaitForFixedUpdate();
            
            VerifyNumberOfSpawns(numberOfSpawns);
        }
    }

    void MovePlayerTo(ItemSpawnPointScript spawnPoint) => _context.MovePlayerTo(spawnPoint);

    static void VerifyNumberOfSpawns(int numberOfSpawns)
    {
        var actualSpawns = GetNumberOfSpawns();
        TestContext.WriteLine($"Spawns: {actualSpawns}");
        Assert.AreEqual(numberOfSpawns, actualSpawns);
    }

    static void VerifyNoSpawns() => Assert.Zero(GetNumberOfSpawns());

    static int GetNumberOfSpawns() => Object.FindObjectsOfType<ItemSpawnScript>().Length;
}