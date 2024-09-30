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
        yield return new WaitForSeconds(2.0f);
        s.stateMachine.Enter("EnemyChaseState"); // TODO: Maybe a "EnemyRecoveringState" to handle logic? Maybe do that here?
    }

    public override void StateEntered()
    {
        Debug.Log("Get them off! Get them off!");
        s.enemyMaterial.color = Color.blue;
        s.StartCoroutine(exitStateAfterTime());
    }
    public override void StateExited()
    {
        Debug.Log("Why, you little--");
    }
}
