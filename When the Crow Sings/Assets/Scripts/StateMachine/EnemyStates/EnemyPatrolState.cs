using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : StateMachineState
{
    EnemyController s;
    public EnemyPatrolState(EnemyController component)
    {
        s = component;
    }
    public override void FixedUpdate()
    {
        //if (Physics.Raycast(s.transform,))
        Debug.Log(ServiceLocator.Get<PlayerController>());
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
