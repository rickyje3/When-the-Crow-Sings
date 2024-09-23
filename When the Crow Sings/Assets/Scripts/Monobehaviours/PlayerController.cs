using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // Paul's variables
    private StateMachine stateMachine;

    // Ricky's variables
    [HideInInspector]
    public DialogueManager dialogueManager;
    public float speed = 5.0f;
    [HideInInspector]
    public Vector3 movementInput;
    public List<DialogueInteract> dialogueInteractables = new List<DialogueInteract>();

    // Paul code
    private void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.RegisterState(new PlayerMovementState(this), "PlayerMovementState");
        stateMachine.Enter("PlayerMovementState");
    }
    private void Update() => stateMachine.Update(Time.deltaTime);
    private void FixedUpdate() => stateMachine.FixedUpdate();

    // Ricky code
    private void Start()
    {
        
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
}