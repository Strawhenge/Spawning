using Strawhenge.Spawning.Unity.Items;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Tests
{
    public static class ItemSpawnScriptExtensions
    {
        public static void MoveSpawn(this ItemSpawnScript spawn)
        {
            spawn.transform.position += new Vector3(1, 0, 9);
            spawn.transform.rotation *= Quaternion.AngleAxis(45, Vector3.up);
        }

        public static void DisplaceParts(this ItemSpawnScript spawn)
        {
            for (var i = 0; i < spawn.Parts.Count; i++)
            {
                var part = spawn.Parts[i];
                part.transform.position += Vector3.forward * i;
                part.transform.rotation = Quaternion.Euler(part.transform.rotation.eulerAngles + Vector3.forward * i);
            }
        }
    }
}