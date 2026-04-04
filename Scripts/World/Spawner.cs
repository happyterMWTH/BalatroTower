using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Variables")]
    [SerializeField] private float enemiesPerSecond;
    [SerializeField] private bool activated = false;
    [SerializeField] private GameObject target;

    void Start()
    {
        Enemy.SetTarget(target);
    }
    
}
