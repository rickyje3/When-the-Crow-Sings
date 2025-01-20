using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

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
        InputManager.playerInputActions.Player.Sprint.canceled += OnSprint;
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
        InputManager.playerInputActions.Player.Sprint.canceled -= OnSprint;
        InputManager.playerInputActions.Player.Crouch.performed -= OnCrouched;

        s.playerAnimator.SetBool("animIsMoving", false);
        s.isSprintingButtonHeld = false;
        s.speed = 8;
    }
    public override void Update(float deltaTime)
    {
        s.ApplyGravity(deltaTime);
        //Converts movement input to a float because vector3 cant be lerped :(((((
        float inputMagnitude = s.movementInput.magnitude;// = Mathf.Clamp(s.movementInput.magnitude,s.minWalkClamp,1.0f);
        float stateClamp = s.minWalkSpeed;
        float stateSpeed = s.maxWalkSpeed;
        float slideSpeedCorrection = s.walkSlideSpeedCorrection;
        if (s.isSprintingButtonHeld && inputMagnitude > 0f)
        {
            s.playerAnimator.SetBool("animIsSprinting", true);

            s.isCrouchingToggled = false;
            s.playerAnimator.SetBool("animIsCrouching", false);
            ResetColliderToDefault();

            stateClamp = s.minSprintSpeed;
            stateSpeed = s.maxSprintSpeed;
            slideSpeedCorrection = s.sprintSlideSpeedCorrection;
        }
        else if (s.isCrouchingToggled)
        {
            s.playerAnimator.SetBool("animIsSprinting", false);
            stateClamp = s.minCrouchSpeed;
            stateSpeed = s.maxCrouchSpeed;
            slideSpeedCorrection = s.crouchSlideSpeedCorrection;
        }
        else
        {
            s.playerAnimator.SetBool("animIsSprinting", false);
        }
        
       
        s.speed = Mathf.Clamp(inputMagnitude * stateSpeed,stateClamp,stateSpeed);
        SetWalkAnimSpeed(s.speed, slideSpeedCorrection);

        // move!!
        Vector3 movement = new Vector3(s.movementInput.x, 0, s.movementInput.y).normalized * s.speed;

        // gravity!!
        movement.y = s.gravityVelocity;

        // Move the character using the CharacterController
        s.characterController.Move(movement * deltaTime);

        // Rotate the player if moving on the XZ plane
        if (movement.x != 0 || movement.z != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
            s.transform.rotation = Quaternion.RotateTowards(s.transform.rotation, toRotation, 1000 * deltaTime);
            s.playerAnimator.SetFloat("currentTurnDelta", 1f);

            s.playerAnimator.SetBool("animIsMoving", true);
        }
        else
        {
            s.playerAnimator.SetBool("animIsMoving", false);
            s.playerAnimator.SetFloat("currentTurnDelta", 0f);
        }
        Debug.Log(s.playerAnimator.GetFloat("currentTurnDelta"));

    }

    private void SetWalkAnimSpeed(float inputMagnitude, float slideSpeedCorrection)
    {
        s.playerAnimator.SetFloat("currentWalkVelocity", inputMagnitude* slideSpeedCorrection);
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            s.isSprintingButtonHeld = true;
        }
        else if (context.canceled)
        {
            s.isSprintingButtonHeld = false;
        }
    }

    private void OnCrouched(InputAction.CallbackContext context)
    {
        s.isCrouchingToggled = !s.isCrouchingToggled;
        s.playerAnimator.SetBool("animIsCrouching", s.isCrouchingToggled);
        if (s.isCrouchingToggled)
        {
            //s.speed = 4;
            s.GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0);
            s.GetComponent<CapsuleCollider>().height = 2;
        }
        else if(!s.isCrouchingToggled)// && !s.isSprintingButtonHeld)
        {
            s.playerAnimator.SetBool("animIsCrouching", false);
            //s.speed = 8;
            ResetColliderToDefault();
        }
    }

    private void ResetColliderToDefault()
    {
        s.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
        s.GetComponent<CapsuleCollider>().height = 4;
    }

    private void OnFired(InputAction.CallbackContext context)
    {
        if (ServiceLocator.Get<GameStateManager>().currentLevelDataLVL.isExterior)
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
