using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyController component) : base(component)
    {
    }

    public override void FixedUpdate()
    {
        s.navMeshAgent.destination = ServiceLocator.Get<PlayerController>().transform.position;
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
