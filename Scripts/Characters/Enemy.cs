using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("AI Variables")]
    [SerializeField] private GameObject target;
    private NavMeshAgent _agent;
    
    private static GameObject _mainTarget;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _agent.SetDestination(_mainTarget.transform.position);
    }

    public static void SetTarget(GameObject target)
    {
        _mainTarget = target;
    }
}
