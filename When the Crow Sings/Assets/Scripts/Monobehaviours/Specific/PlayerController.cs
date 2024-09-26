using System.Collections.Generic;
using UnityEngine;


public class PlayerController : StateMachineComponent, IService
{
    // Paul code
    public Transform throwPosition;
    [SerializeField]
    private Transform pfBirdseedProjectile;
    private void Awake()
    {
        register_self();

        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new PlayerMovementState(this), "PlayerMovementState");
        stateMachine.RegisterState(new PlayerThrowBirdseedState(this), "PlayerThrowBirdseedState");
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

    public void register_self()
    {
        ServiceLocator.Register<PlayerController>(this);
    }


    public void ThrowBirdseed()
    {
        Instantiate(pfBirdseedProjectile);
    }

}