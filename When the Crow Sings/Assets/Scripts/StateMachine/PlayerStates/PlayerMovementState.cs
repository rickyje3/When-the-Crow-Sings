using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : StateMachineState
{
    PlayerController s;
    public PlayerMovementState(PlayerController component)
    {
        s = component;
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

        playerInput.Player.Action.performed += OnAction;
        playerInput.Player.Action.canceled += OnAction;

        playerInput.Player.Interact.performed += OnInteract;
        playerInput.Player.Interact.canceled += OnInteract;
    }

    public override void StateExited()
    {
        // Unsubscribe from events
        var playerInput = new PlayerInputActions();
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMove;

        playerInput.Player.Action.performed -= OnAction;
        playerInput.Player.Action.canceled -= OnAction;

        playerInput.Player.Interact.performed -= OnInteract;
        playerInput.Player.Interact.canceled -= OnInteract;

        playerInput.Player.Disable();
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
        // Get the input vector from the action map
        //s.movementInput = context.ReadValue<Vector2>();
        if (s.dialogueManager.choicesShown)
        {
            Vector2 input = context.ReadValue<Vector2>();

            // Check if input is up or down to navigate choices
            if (input.y > 0) // Move up
            {
                s.dialogueManager.HandleChoiceSelection(false); // Move up in choices
            }
            else if (input.y < 0) // Move down
            {
                s.dialogueManager.HandleChoiceSelection(true); // Move down in choices
            }
        }
        else
        {
            s.movementInput = context.ReadValue<Vector2>(); // Normal movement

        }
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && s.dialogueManager != null && s.dialogueManager.choicesShown)
        {
            // Confirm the currently selected choice
            s.dialogueManager.ConfirmChoice();
        }
        else if (context.performed && s.dialogueManager != null && s.dialogueManager.choicesShown == false)
        {
            s.dialogueManager.DisplayNextSentence();
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {

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
