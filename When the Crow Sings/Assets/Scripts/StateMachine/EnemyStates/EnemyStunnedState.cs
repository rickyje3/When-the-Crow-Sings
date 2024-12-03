using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnedState : EnemyState
{
    public EnemyStunnedState(EnemyController component) : base(component)
    {
    }

    private IEnumerator exitStateAfterTime()
    {
        s.navMeshAgent.destination = s.transform.position;
        yield return new WaitForSeconds(s.timeToBeStunned);
        //s.stateMachine.Enter("EnemyChaseState"); // TODO: Maybe a "EnemyRecoveringState" to handle logic? Maybe do that here?
        //s.stateMachine.EnterPrevious();
        s.stateMachine.Enter("EnemyIdleState");
    }

    public override void StateEntered()
    {
        s.enemyAnimator.SetBool("animIsStunned", true);
        Debug.Log("Get them off! Get them off!");
        s.StartCoroutine(exitStateAfterTime());
    }
    public override void StateExited()
    {
        s.enemyAnimator.SetBool("animIsStunned", false);
        Debug.Log("Why, you little--");
    }
}
