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
        s.playerAnimator.SetLayerWeight(1, 1f);
    }
    public override void StateExited()
    {
        s.playerAnimator.SetLayerWeight(1, 0f);
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
