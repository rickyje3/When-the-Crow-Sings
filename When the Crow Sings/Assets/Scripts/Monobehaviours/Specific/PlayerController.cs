using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : StateMachineComponent, IService
{
    // Paul code
    public Transform throwPosition;
    public GameObject throwTarget;
    public Animator playerAnimator;
    [SerializeField]
    private BirdseedController pfBirdseedProjectile;
    [HideInInspector]
    public bool isCrouching = false;

    private void Awake()
    {
        RegisterSelfAsService();

        playerInput = new PlayerInputActions();

        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new PlayerMovementState(this), "PlayerMovementState");
        stateMachine.RegisterState(new PlayerThrowBirdseedState(this), "PlayerThrowBirdseedState");
        stateMachine.Enter("PlayerMovementState");
    }

    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<PlayerController>(this);
    }


    public void ThrowBirdseed()
    {
        var direction = throwTarget.transform.position - transform.position;
        BirdseedController.Create(pfBirdseedProjectile, throwPosition, direction);
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