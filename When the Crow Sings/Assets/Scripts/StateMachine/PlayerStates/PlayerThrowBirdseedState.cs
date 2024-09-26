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
        Debug.Log("Throw it now!!");
        s.ThrowBirdseed();

        s.StartCoroutine(ExitStateAfterDelay());
    }


    private IEnumerator ExitStateAfterDelay()
    {
        Debug.Log("Timer's started");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Timer's over");
        s.stateMachine.Enter("PlayerMovementState");
    }

    public override void StateExited()
    {
        Debug.Log("It's thrown...");
    }

    public override void Update(float deltaTime)
    {
        //Debug.Log("State 2 is updating!");
    }
}
