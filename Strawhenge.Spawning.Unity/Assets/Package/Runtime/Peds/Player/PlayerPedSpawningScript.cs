using Strawhenge.Common.Unity;
using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Common.Unity.Serialization;
using Strawhenge.Spawning.Unity.Peds.Settings;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds
{
    public class PlayerPedSpawningScript : BasePlayerPedSpawningScript
    {
        [SerializeField] Camera _camera;

        [SerializeField]
        SerializedSource<IPedSpawnSettings, SerializedPedSpawnSettings, PedSpawnSettingsScriptableObject> _settings;

        SpawnChecker _spawnChecker;

        void Awake()
        {
            ComponentRefHelper.EnsureCamera(ref _camera, nameof(_camera), this);

            var rigidBody = this.GetOrAddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
            
            var collider = GetComponent<Collider>();
            if (collider == null) collider = this.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            
            _spawnChecker = new SpawnChecker(_camera, gameObject, _settings.GetValue());
        }

        public override bool CanSpawn(Vector3 position) => _spawnChecker.CanSpawn(position);

        public override bool CanSpawnInEntrance(Vector3 position) => _spawnChecker.CanSpawnInEntrance(position);

        public override bool CanDespawn(GameObject gameObject) => _spawnChecker.CanDespawn(gameObject);

        public override bool IsInRadius(Vector3 position) => _spawnChecker.IsInRadius(position);
    }
}