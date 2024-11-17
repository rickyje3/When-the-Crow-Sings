using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowIdleState : StateMachineState
{
    BirdBrain s;

    public CrowIdleState(BirdBrain birdBrain)
    {
        s = birdBrain;
    }

    public override void StateEntered()
    {
        //s.StartCoroutine(ExitStateAfterTime());
        
    }

    IEnumerator ExitStateAfterTime()
    {
        yield return new WaitForSeconds(1);
        s.stateMachine.Enter("CrowScatterState");
    }
}
