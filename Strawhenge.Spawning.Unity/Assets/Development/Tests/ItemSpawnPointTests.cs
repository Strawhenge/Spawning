using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ItemSpawnPointTests
{
    const string Scene = "ItemSpawnPointTests";
    TestContextScript _context;

    [UnitySetUp]
    public IEnumerator LoadScene()
    {
        yield return SceneManager.LoadSceneAsync(Scene);
        _context = Object.FindObjectOfType<TestContextScript>();

        if (_context == null || _context.IsInvalid())
            Assert.Fail("Test context is invalid.");
    }

    [UnityTest]
    public IEnumerator Items_should_spawn_when_player_is_near_spawn_points()
    {
        SpawnsHelper.VerifyNoSpawns();
        yield return new WaitForFixedUpdate();

        yield return SpawnPointShouldSpawnWhenPlayerMovesNear();
        yield return SpawnPointsShouldNotRespawnWhenPlayerReturns();
    }

    IEnumerator SpawnPointShouldSpawnWhenPlayerMovesNear()
    {
        int expectedNumberOfSpawns = 0;

        foreach (var spawnPoint in _context.SpawnPoints)
        {
            _context.MovePlayerTo(spawnPoint);
            yield return new WaitForFixedUpdate();

            expectedNumberOfSpawns++;
            SpawnsHelper.VerifyNumberOfSpawns(expectedNumberOfSpawns);
        }
    }

    IEnumerator SpawnPointsShouldNotRespawnWhenPlayerReturns()
    {
        var numberOfSpawns = SpawnsHelper.GetNumberOfSpawns();

        foreach (var spawnPoint in _context.SpawnPoints)
        {
            _context.MovePlayerTo(spawnPoint);
            yield return new WaitForFixedUpdate();

            SpawnsHelper.VerifyNumberOfSpawns(numberOfSpawns);
        }
    }
}