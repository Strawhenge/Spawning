using Strawhenge.Spawning.Unity.Items;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Editor
{
    public class ItemSpawnMakerWizard : ScriptableWizard
    {
        const string Name = "Item Spawn Maker";

        [MenuItem("Strawhenge/" + Name)]
        public static void ShowEditorWindow()
        {
            DisplayWizard<ItemSpawnMakerWizard>(Name);
        }

        [SerializeField] string _name = "Item Spawn";
        [SerializeField] ItemSpawnPartScript[] _parts;

        void OnWizardCreate()
        {
            var spawn = new GameObject(_name).AddComponent<ItemSpawnScript>();

            var serialized = new SerializedObject(spawn);
            var property = serialized.FindProperty(ItemSpawnScript.PartsFieldName);

            property.arraySize = _parts.Length;

            for (var i = 0; i < property.arraySize; i++)
            {
                property.GetArrayElementAtIndex(i).objectReferenceValue = _parts[i];
            }

            serialized.ApplyModifiedProperties();
        }
    }
}