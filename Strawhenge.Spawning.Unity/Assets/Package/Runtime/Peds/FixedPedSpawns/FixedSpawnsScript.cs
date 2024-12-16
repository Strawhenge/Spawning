using Strawhenge.Common;
using UnityEngine;

namespace Strawhenge.Spawning.Unity.Peds.FixedPedSpawns
{
    public class FixedSpawnsScript : MonoBehaviour
    {
        [SerializeField] GameObject _player;

        FixedSpawnPointScript[] _fixedSpawnPoints;

        public ILayersAccessor LayerAccessor { private get; set; }

        void Awake()
        {
            if (_player == null)
                Debug.LogError($"'{nameof(_player)}' not set.", this);

            _fixedSpawnPoints = GetComponentsInChildren<FixedSpawnPointScript>();
        }

        void Start()
        {
            gameObject.layer = LayerAccessor.PedSpawnTriggersLayer;
        }

        [ContextMenu(nameof(Spawn))]
        void Spawn()
        {
            _fixedSpawnPoints
                .ForEach(x => x.Spawn());
        }

        [ContextMenu(nameof(Despawn))]
        void Despawn()
        {
            _fixedSpawnPoints
                .ForEach(x => x.Despawn());
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _player)
                Spawn();
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _player)
                Despawn();
        }
    }
}