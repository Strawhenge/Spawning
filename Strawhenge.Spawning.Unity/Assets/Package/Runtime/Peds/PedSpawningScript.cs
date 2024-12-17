using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Common.Unity.Serialization;
using Strawhenge.Spawning.Unity.Peds.Settings;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds
{
    public class PedSpawningScript : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] GameObject _player;

        [SerializeField]
        SerializedSource<IPedSpawnSettings, SerializedPedSpawnSettings, PedSpawnSettingsScriptableObject> _settings;

        public SpawnChecker SpawnChecker { private get; set; }

        void Start()
        {
            ComponentRefHelper.EnsureCamera(ref _camera, nameof(_camera), this);

            if (_player == null)
            {
                Debug.LogWarning($"'{_player}' not assigned. Searching by tag.");

                _player = GameObject.FindGameObjectWithTag("Player");
                if (_player == null)
                {
                    Debug.LogError($"'{_player}' not found. Using '{gameObject}' instead.");
                    _player = gameObject;
                }
            }

            SpawnChecker.Setup(_camera, _player, _settings.GetValue());
        }

        void OnDestroy()
        {
            SpawnChecker.Reset();
        }
    }
}