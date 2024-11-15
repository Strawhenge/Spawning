using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Strawhenge.Spawning.Unity.Tests
{
    public abstract class BaseTest<TContext> where TContext : BaseTestContextScript
    {
        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            yield return SceneManager.LoadSceneAsync(SceneName);
            Context = Object.FindObjectOfType<TContext>();

            if (Context == null || Context.IsInvalid())
                Assert.Fail("Test context is invalid.");
        }

        protected abstract string SceneName { get; }

        protected TContext Context { get; private set; }
    }
}