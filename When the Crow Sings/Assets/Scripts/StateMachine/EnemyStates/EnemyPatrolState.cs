using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(EnemyController component) : base(component)
    {
    }

    private void Patrol()
    {

    }








    public override void FixedUpdate()
    {
        //if (Physics.Raycast(s.transform,))
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("I SEE YOU THERE IS NO LIFE IN THE VOID DIE NOW");
        s.stateMachine.Enter("EnemyChaseState");
    }

    public override void OnDisable()
    {

    }

    public override void OnEnable()
    {

    }

    public override void StateEntered()
    {

    }

    public override void StateExited()
    {

    }

    public override void Update(float deltaTime)
    {

    }
}
