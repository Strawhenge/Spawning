using Strawhenge.Common.Unity;
using UnityEngine;

namespace Strawhenge.Spawning.Unity
{
    public class PlayerRadiusScript : MonoBehaviour
    {
        [SerializeField, Min(1)] float _radius = 25;

        void Awake()
        {
            var rigidBody = this.GetOrAddComponent<Rigidbody>();
            rigidBody.isKinematic = true;

            var collider = this.GetOrAddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = _radius / Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);
        }

        internal bool IsInRadius(Transform transform)
        {
            return Vector3.Distance(transform.position, this.transform.position) <= _radius;
        }
    }
}