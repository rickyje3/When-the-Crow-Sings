using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyController component) : base(component)
    {
    }

    public override void FixedUpdate()
    {
        if (s.doesSeePlayer)
        {
            s.stateMachine.Enter("EnemyChaseState");
        }
    }

    private IEnumerator exitStateAfterTime()
    {
        s.navMeshAgent.destination = s.transform.position;
        yield return new WaitForSeconds(s.timeToWaitBetweenWander);
        s.stateMachine.Enter("EnemyPatrolState");
    }

    public override void StateEntered()
    {
        s.enemyAnimator.SetBool("animIsIdle", true);
        s.StartCoroutine(exitStateAfterTime());
    }
    public override void StateExited()
    {
        s.enemyAnimator.SetBool("animIsIdle", false);
    }
}
