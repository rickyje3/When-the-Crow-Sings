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
        InputManager.playerInputActions.Player.Fire.canceled += OnFire;
        s.throwTarget.SetActive(true);
    }
    public override void StateExited()
    {
        
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        InputManager.playerInputActions.Player.Fire.canceled -= OnFire;
        s.throwTarget.SetActive(false);
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
