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
        //Debug.Log("Entering MovementState");

        // Subscribe to the Move event in the input system
        InputManager.playerInputActions.Player.Move.performed += OnMove;
        InputManager.playerInputActions.Player.Move.canceled += OnMove;

        InputManager.playerInputActions.Player.Action.performed += OnAction;
        InputManager.playerInputActions.Player.Action.canceled += OnAction;

        InputManager.playerInputActions.Player.Interact.performed += OnInteract;
        InputManager.playerInputActions.Player.Interact.canceled += OnInteract;

        InputManager.playerInputActions.Player.Fire.performed += OnFired;

        InputManager.playerInputActions.Player.Sprint.performed += OnSprint;
        InputManager.playerInputActions.Player.Crouch.performed += OnCrouched;
    }

    public override void StateExited()
    {
        //s.playerInput.Player.Move.performed -= OnMove;
        //s.playerInput.Player.Move.canceled -= OnMove;

        InputManager.playerInputActions.Player.Action.performed -= OnAction;
        InputManager.playerInputActions.Player.Action.canceled -= OnAction;

        InputManager.playerInputActions.Player.Interact.performed -= OnInteract;
        InputManager.playerInputActions.Player.Interact.canceled -= OnInteract;

        InputManager.playerInputActions.Player.Fire.performed -= OnFired;

        InputManager.playerInputActions.Player.Sprint.performed -= OnSprint;
        InputManager.playerInputActions.Player.Crouch.performed -= OnCrouched;

        s.playerAnimator.SetBool("animIsMoving", false);
        s.isSprinting = false;
        s.speed = 8;
    }



    public override void Update(float deltaTime)
    {
        // Apply gravity to velocity
        s.velocity += s.gravity * s.gravityMultiplier * deltaTime;

        //Converts movement input to a float because vector3 cant be lerped :(((((
        float inputMagnitude = s.movementInput.magnitude;

        if (!s.isSprinting)
        {
            //Smoothly blend speed off of joystick input (8 is the max walking speed)
            s.speed = Mathf.Lerp(s.speed, s.movementInput.magnitude * 8, Time.deltaTime * s.acceleration);
        }

        // move!!
        Vector3 movement = new Vector3(s.movementInput.x, 0, s.movementInput.y).normalized * s.speed;

        // gravity!!
        movement.y = s.velocity;

        // Move the character using the CharacterController
        s.characterController.Move(movement * deltaTime);

        if (s.characterController.isGrounded && s.velocity < 0)
        {
            s.velocity = 0; // Reset vertical velocity
        }

        // Rotate the player if moving on the XZ plane
        if (movement.x != 0 || movement.z != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
            s.transform.rotation = Quaternion.RotateTowards(s.transform.rotation, toRotation, 1000 * deltaTime);

            s.playerAnimator.SetBool("animIsMoving", true);
        }
        else
        {
            s.playerAnimator.SetBool("animIsMoving", false);
        }

    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            if (!s.isCrouching)
            {
                s.isSprinting = !s.isSprinting;
                s.playerAnimator.SetBool("animIsSprinting", s.isSprinting);
                s.isSprinting = true;
                s.speed = 14;
                Debug.Log("issprinting");
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.JoystickButton1))
        {
            if (!s.isCrouching)
            {
                s.isSprinting = false;
                s.speed = 8;
            }
        }
    }

    private void OnCrouched(InputAction.CallbackContext context)
    {
        s.isCrouching = !s.isCrouching;
        s.playerAnimator.SetBool("animIsCrouching", s.isCrouching);
        if (s.isCrouching)
        {
            s.speed = 4;
            //s.GetComponent<CapsuleCollider>().center.Set(0,0,0);
            s.GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0);
            s.GetComponent<CapsuleCollider>().height = 2;
        }
        else
        {
            s.speed = 8;
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
       //if (context.performed)
       //{
            //if (s.qteInteract.playerInRange)
            //{
            //activate when interact key is pressed
            //s.qteInteract.ActivateTimingMeter();
            //}
       //}
    }
}
