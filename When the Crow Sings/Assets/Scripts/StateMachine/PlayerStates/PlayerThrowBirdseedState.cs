using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowBirdseedState : StateMachineState
{
    PlayerController s;
    public PlayerThrowBirdseedState(PlayerController component)
    {
        s = component;
    }

    public override void StateEntered()
    {
        s.playerInput.Player.Fire.canceled += OnAction;
    }
    public override void StateExited()
    {
        s.playerInput.Player.Fire.canceled -= OnAction;
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        Debug.Log("Actioned!");
        s.ThrowBirdseed();
        s.StartCoroutine(ExitStateAfterDelay());
    }


    private IEnumerator ExitStateAfterDelay()
    {
        yield return new WaitForSeconds(.25f);
        s.stateMachine.Enter("PlayerMovementState");
    }



    public override void Update(float deltaTime)
    {
        //Debug.Log("State 2 is updating!");
    }
}
