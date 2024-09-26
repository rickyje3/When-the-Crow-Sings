using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : StateMachineComponent
{
    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new EnemyPatrolState(this), "EnemyPatrolState");
        stateMachine.RegisterState(new EnemyChaseState(this), "EnemyChaseState");
        stateMachine.Enter("EnemyPatrolState");
    }
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void TriggerEntered(Collider other)
    {
        stateMachine.OnTriggerEnter(other);
    }
    public void TriggerExited(Collider other)
    {
       stateMachine.OnTriggerExit(other);
    }
}
