using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class PlayerItemSpawnRadiusScript : MonoBehaviour
    {
        [SerializeField] SphereCollider _collider;

        float _radius;

        void Awake()
        {
            if (_collider == null)
            {
                Debug.LogWarning($"'{nameof(_collider)}' not set. Getting from GameObject.", this);

                _collider = GetComponent<SphereCollider>();
                if (_collider == null)
                {
                    Debug.LogWarning($"'{nameof(_collider)}' missing.", this);
                    return;
                }
            }

            _radius = _collider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);
            Debug.Log($"Player item spawn radius: {_radius}", this);
        }

        internal bool IsInRadius(Transform transform)
        {
            return Vector3.Distance(transform.position, this.transform.position) <= _radius;
        }
    }
}