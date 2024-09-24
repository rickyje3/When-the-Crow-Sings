using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : StateMachineComponent
{
    public MeshCollider sightCone;
    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.RegisterState(new EnemyPatrolState(this), "EnemyPatrolState");
        stateMachine.RegisterState(new EnemyChaseState(this), "EnemyChaseState");
        stateMachine.Enter("EnemyChaseState");
    }
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
