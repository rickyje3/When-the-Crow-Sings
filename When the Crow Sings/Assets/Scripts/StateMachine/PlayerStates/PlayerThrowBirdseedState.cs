using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowBirdseedState : StateMachineState
{
    PlayerController s;
    bool canThrow = false;


    public PlayerThrowBirdseedState(PlayerController component)
    {
        s = component;
    }

    public override void StateEntered()
    {
        s.StartCoroutine(WaitBeforeThrowing());
        s.playerAnimator.SetLayerWeight(1, 1f);
        InputManager.playerInputActions.Player.Fire.canceled += OnFire;

        s.throwTarget.SetActive(true);
        
    }
    public override void StateExited()
    {
        s.playerAnimator.SetLayerWeight(1, 0f);
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        s.StartCoroutine(ThrowBirdseedOnceAble());
    }

    private IEnumerator WaitBeforeThrowing()
    {
        canThrow = false;
        yield return new WaitForSeconds(.25f);
        canThrow = true;
    }

    private IEnumerator ThrowBirdseedOnceAble()
    {
        InputManager.playerInputActions.Player.Fire.canceled -= OnFire;

        while (!canThrow) yield return null;
        
        
        s.ThrowBirdseed();

        s.playerAnimator.SetTrigger("animThrow");

        s.throwTarget.SetActive(false);
    }



    public override void Update(float deltaTime)
    {
        //Debug.Log("State 2 is updating!");
    }
}
