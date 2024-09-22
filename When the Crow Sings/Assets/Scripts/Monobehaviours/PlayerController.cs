using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 movementInput;

    public List<DialogueInteract> dialogueInteractables = new List<DialogueInteract>();

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
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        var playerInput = new PlayerInputActions();
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMove;

        playerInput.Player.Interact.performed -= OnInteract;
        playerInput.Player.Interact.canceled -= OnInteract;

        playerInput.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Get the input vector from the action map
        movementInput = context.ReadValue<Vector2>();
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


