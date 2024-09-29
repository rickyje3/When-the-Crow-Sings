using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : StateMachineState
{
    PlayerController s;
    QTEInteract qte;

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
        else if (context.performed && s.dialogueManager != null && s.dialogueManager.choicesShown == false && s.dialogueManager.inDialogue)
        {
            s.dialogueManager.DisplayNextSentence();
        }
        else if (context.performed && s.dialogueManager != null && s.dialogueManager.choicesShown == false && s.dialogueManager.inDialogue == false)
        {

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

        if (context.performed)
        {
            if (s.qteInteract.playerInRange)
            {
                 //activate when interact key is pressed
                 s.qteInteract.ActivateTimingMeter();
            }
        }
    }
}
