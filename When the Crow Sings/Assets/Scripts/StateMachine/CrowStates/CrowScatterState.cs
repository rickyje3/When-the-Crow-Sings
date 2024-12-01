using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowScatterState : StateMachineState
{
    BirdBrain s;
    public CrowScatterState(BirdBrain birdBrain)
    {
        s = birdBrain;
    }
    
    public override void FixedUpdate()
    {
        s.FlyNavigate();
    }

    public override void StateEntered()
    {
        s.dir = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) *.1f;
        s.StartCoroutine(WaitThenEnterTargetState());
        s.crowAnimator.SetBool("isFlying", true);
        s.crowAnimator.SetBool("isIdle", false);
        s.crowAnimator.SetBool("isPecking", false);
    }

    IEnumerator WaitThenEnterTargetState()
    {
        yield return new WaitForSeconds(1.5f);
        s.stateMachine.Enter("CrowTargetState");
    }
}
