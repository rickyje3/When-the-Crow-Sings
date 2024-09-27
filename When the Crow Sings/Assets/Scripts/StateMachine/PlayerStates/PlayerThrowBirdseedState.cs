using System.Collections;
using UnityEngine;

public class PlayerThrowBirdseedState : StateMachineState
{
    PlayerController s;
    public PlayerThrowBirdseedState(PlayerController component)
    {
        s = component;
    }

    public override void StateEntered()
    {
        s.ThrowBirdseed();

        s.StartCoroutine(ExitStateAfterDelay());
    }


    private IEnumerator ExitStateAfterDelay()
    {
        yield return new WaitForSeconds(.25f);
        s.stateMachine.Enter("PlayerMovementState");
    }

    public override void StateExited()
    {
    }

    public override void Update(float deltaTime)
    {
        //Debug.Log("State 2 is updating!");
    }
}
