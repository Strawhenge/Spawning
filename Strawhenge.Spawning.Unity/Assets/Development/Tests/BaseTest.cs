using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests
{
    public abstract class BaseTest
    {
        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            yield return SceneManager.LoadSceneAsync(SceneName);
            Context = Object.FindObjectOfType<TestContextScript>();

            if (Context == null || Context.IsInvalid())
                Assert.Fail("Test context is invalid.");
        }

        protected abstract string SceneName { get; }

        protected TestContextScript Context { get; private set; }
    }
}