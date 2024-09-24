using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : StateMachineState
{
    public PlayerMovementState(StateMachine stateMachine, PlayerController2 component) : base(stateMachine, component) // Constructor.
    {
        
    }

    public override void FixedUpdate()
    {
        
    }

    public override void StateEntered()
    {
        // Enable the Input System
        var playerInput = new PlayerInputActions();
        playerInput.Player.Enable();

        // Subscribe to the Move event in the input system
        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OnMove;

        playerInput.Player.Interact.performed += OnInteract;
        playerInput.Player.Interact.canceled += OnInteract;
    }

    public override void StateExited()
    {
        // Unsubscribe from events
        var playerInput = new PlayerInputActions();
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMove;

        playerInput.Player.Interact.performed -= OnInteract;
        playerInput.Player.Interact.canceled -= OnInteract;

        playerInput.Player.Disable();
    }

    public override void Update(float deltaTime)
    {
        
        PlayerController2 s = (PlayerController2)component;
        // Move!!!
        Vector3 movement = new Vector3(s.movementInput.x, 0, s.movementInput.y).normalized * s.speed * Time.deltaTime;
        s.transform.position += movement;

        //Rotate!!!
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            s.transform.rotation = Quaternion.RotateTowards(s.transform.rotation, toRotation, s.speed * Time.deltaTime);
        }
    }

    

    public override void OnEnable()
    {
        
    }

    public override void OnDisable()
    {
        
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        PlayerController2 s = (PlayerController2)component;
        // Get the input vector from the action map
        s.movementInput = context.ReadValue<Vector2>();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        PlayerController2 s = (PlayerController2)component;

        if (context.performed)
        {
            foreach (var interactable in s.dialogueInteractables)
            {
                if (interactable.playerInRange)
                {
                    //activate when interact key is pressed
                    interactable.ActivateDialogue();
                    break; // Exit after activating the first available dialogue
                }
            }
        }
    }
}