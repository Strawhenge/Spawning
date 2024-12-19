using System;
using System.Collections;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Helpers
{
    static class CoroutineHelper
    {
        public static IEnumerator DoWhen(Action action, Func<bool> predicate, float checkFrequencyInSeconds)
        {
            var wait = new WaitForSeconds(checkFrequencyInSeconds);

            while (!predicate())
            {
                yield return wait;
            }

            action();
        }
    }
}