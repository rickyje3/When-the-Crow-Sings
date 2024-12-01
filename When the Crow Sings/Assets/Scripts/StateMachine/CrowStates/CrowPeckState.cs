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

    public override void FixedUpdate()
    {
        s.StillGravity();
    }

    public override void StateEntered()
    {
        s.transform.localRotation = Quaternion.Euler(0f, s.transform.localRotation.eulerAngles.y, s.transform.localRotation.eulerAngles.z);

        s.crowAnimator.SetBool("isFlying", false);
        s.crowAnimator.SetBool("isIdle", false);
        s.crowAnimator.SetBool("isPecking", true);
    }

    IEnumerator ExitStateAfterSeconds()
    {
        yield return new WaitForSeconds(s.secondsToPeck);
        // TODO: Figure out best place for "decide if it should look for more birdseed or not"
        s.stateMachine.Enter("CrowIdleState");
    }
}
