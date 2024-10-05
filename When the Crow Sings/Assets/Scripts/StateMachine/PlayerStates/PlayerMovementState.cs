using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : StateMachineState
{
    PlayerController s;

    public PlayerMovementState(PlayerController component)
    {
        s = component;
    }


    public override void StateEntered()
    {

        // Subscribe to the Move event in the input system
        s.playerInput.Player.Move.performed += OnMove;
        s.playerInput.Player.Move.canceled += OnMove;

        s.playerInput.Player.Action.performed += OnAction;
        s.playerInput.Player.Action.canceled += OnAction;

        s.playerInput.Player.Interact.performed += OnInteract;
        s.playerInput.Player.Interact.canceled += OnInteract;

        s.playerInput.Player.Fire.performed += OnFired;

        s.playerInput.Player.Crouch.performed += OnCrouched;
    }

    public override void StateExited()
    {
        //s.playerInput.Player.Move.performed -= OnMove;
        //s.playerInput.Player.Move.canceled -= OnMove;

        s.playerInput.Player.Action.performed -= OnAction;
        s.playerInput.Player.Action.canceled -= OnAction;

        s.playerInput.Player.Interact.performed -= OnInteract;
        s.playerInput.Player.Interact.canceled -= OnInteract;

        s.playerInput.Player.Fire.performed -= OnFired;

        s.playerInput.Player.Crouch.performed -= OnCrouched;

        s.playerAnimator.SetBool("animIsMoving", false);
    }



    public override void Update(float deltaTime)
    {
        // Move!!!
        Vector3 movement = new Vector3(s.movementInput.x, 0, s.movementInput.y).normalized * s.speed * Time.deltaTime;
        s.transform.position += movement;

        //Rotate!!!
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            s.transform.rotation = Quaternion.RotateTowards(s.transform.rotation, toRotation, 1000 * Time.deltaTime);

            s.playerAnimator.SetBool("animIsMoving", true);
        }
        else
        {
            s.playerAnimator.SetBool("animIsMoving", false);
        }
    }

    private void OnCrouched(InputAction.CallbackContext context)
    {
        s.isCrouching = !s.isCrouching;
        s.playerAnimator.SetBool("animIsCrouching", s.isCrouching);
        if (s.isCrouching)
        {
            //s.GetComponent<CapsuleCollider>().center.Set(0,0,0);
            s.GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0);
            s.GetComponent<CapsuleCollider>().height = 2;
        }
        else
        {
            //s.GetComponent<CapsuleCollider>().center.Set(0, 1, 0);
            s.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
            s.GetComponent<CapsuleCollider>().height = 4;
        }
    }
    private void OnFired(InputAction.CallbackContext context)
    {
        s.stateMachine.Enter("PlayerThrowBirdseedState");
    }

    private void OnMove(InputAction.CallbackContext context)
    {
         s.movementInput = context.ReadValue<Vector2>(); // Normal movement
        
    }

    private void OnAction(InputAction.CallbackContext context)
    {
    }

    private void OnInteract(InputAction.CallbackContext context)
    {

    }
}
