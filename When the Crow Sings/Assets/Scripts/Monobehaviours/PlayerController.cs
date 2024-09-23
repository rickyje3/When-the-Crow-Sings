using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 movementInput;
    private DialogueManager dialogueManager;

    public List<DialogueInteract> dialogueInteractables = new List<DialogueInteract>();

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnEnable()
    {
        // Enable the Input System
        var playerInput = new PlayerInputActions();
        playerInput.Player.Enable();

        // Subscribe to the Move event in the input system
        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OnMove;

        playerInput.Player.Interact.performed += OnInteract;
        playerInput.Player.Interact.canceled += OnInteract;

        playerInput.Player.Action.performed += OnAction;
        playerInput.Player.Action.canceled += OnAction;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        var playerInput = new PlayerInputActions();
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMove;

        playerInput.Player.Interact.performed -= OnInteract;
        playerInput.Player.Interact.canceled -= OnInteract;

        playerInput.Player.Action.performed += OnAction;
        playerInput.Player.Action.canceled += OnAction;

        playerInput.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
{
    if (dialogueManager.choicesShown)
    {
        Vector2 input = context.ReadValue<Vector2>();
        
        // Check if input is up or down to navigate choices
        if (input.y > 0) // Move up
        {
            dialogueManager.HandleChoiceSelection(false); // Move up in choices
        }
        else if (input.y < 0) // Move down
        {
            dialogueManager.HandleChoiceSelection(true); // Move down in choices
        }
    }
    else
    {
        movementInput = context.ReadValue<Vector2>(); // Normal movement
    }
}


    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            foreach (var interactable in dialogueInteractables)
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

    private void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && dialogueManager != null && dialogueManager.choicesShown)
        {
            // Confirm the currently selected choice
            dialogueManager.ConfirmChoice();
        }
        else if (context.performed && dialogueManager != null && dialogueManager.choicesShown == false)
        {
            dialogueManager.DisplayNextSentence();
        }
    }


    private void Update()
    {
        // Move!!!
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y).normalized * speed * Time.deltaTime;
        transform.position += movement;

        //Rotate!!!
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);
        } 
    }
}


