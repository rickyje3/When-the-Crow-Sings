using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowPeckState : StateMachineState
{
    BirdBrain s;
    public CrowPeckState(BirdBrain birdBrain)
    {
        s = birdBrain;
    }


    public override void StateEntered()
    {
        base.StateEntered();
    }

    IEnumerator ExitStateAfterSeconds()
    {
        yield return new WaitForSeconds(s.secondsToPeck);
        // TODO: Figure out best place for "decide if it should look for more birdseed or not"
        s.stateMachine.Enter("CrowIdleState");
    }
}
