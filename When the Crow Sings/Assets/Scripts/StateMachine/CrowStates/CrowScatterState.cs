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
        float range = 20f;

        s.distanceToDestination = 10f;

        s.destination = new Vector3(Random.Range(-range,range), Random.Range(range, range), Random.Range(-range, range));
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
