using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : StateMachineComponent, IService
{
    // Paul code
    public Transform throwPosition;
    public GameObject throwTarget;
    [SerializeField]
    private BirdseedController pfBirdseedProjectile;
    private void Awake()
    {
        RegisterSelfAsService();

        playerInput = new PlayerInputActions();

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

    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<PlayerController>(this);
    }


    public void ThrowBirdseed()
    {
        BirdseedController.Create(pfBirdseedProjectile, throwPosition, new Vector3(1,0,1));
    }

    public PlayerInputActions playerInput;
    private void OnEnable()
    {
        playerInput.Player.Enable();
    }
    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

}