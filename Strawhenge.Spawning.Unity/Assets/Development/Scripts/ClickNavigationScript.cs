using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ClickNavigationScript : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    Camera _camera;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0) || !Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
            return;

        _navMeshAgent.SetDestination(hit.point);
    }
}