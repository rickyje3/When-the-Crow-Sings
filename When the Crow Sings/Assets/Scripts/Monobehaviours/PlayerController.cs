using System.Collections.Generic;
using UnityEngine;


public class PlayerController : StateMachineComponent
{
    // Paul code
    private void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.RegisterState(new PlayerMovementState(this), "PlayerMovementState");
        stateMachine.Enter("PlayerMovementState");
    }
    

    // Ricky code

    [HideInInspector]
    public DialogueManager dialogueManager;
    public float speed = 5.0f;
    [HideInInspector]
    public Vector3 movementInput;
    public List<DialogueInteract> dialogueInteractables = new List<DialogueInteract>();

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
}