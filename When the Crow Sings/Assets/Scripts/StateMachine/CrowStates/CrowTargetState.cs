using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowTargetState : StateMachineState
{
    BirdBrain s;
    public CrowTargetState(BirdBrain birdBrain)
    {
        s = birdBrain;
    }

    public override void FixedUpdate()
    {
        //s.targetPosition = (s.destination - s.transform.position)*.01f;
        s.FlyNavigate_FixedUpdate();
    }
    public override void StateEntered()
    {
        if (s.targetIsTargetNotSpawn) s.destination = s.crowHolder.CrowTarget.transform.position;
        else s.destination = s.restPoint.transform.position;
        s.crowAnimator.SetBool("isFlying", true);
        s.crowAnimator.SetBool("isIdle", false);
        s.crowAnimator.SetBool("isPecking", false);
    }
}
