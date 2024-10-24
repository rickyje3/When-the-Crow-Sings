using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


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
    [HideInInspector]
    public bool isSprinting = false;
    [HideInInspector]
    public float gravity = -9.81f;
    [HideInInspector]
    public float gravityMultiplier = 3f;
    [HideInInspector]
    public float velocity;
    public CharacterController characterController;
    public QTEInteract qteInteract;

    private void Awake()
    {
        RegisterSelfAsService();

        characterController = GetComponent<CharacterController>();

        if(qteInteract != null)
        qteInteract = FindObjectOfType<QTEInteract>();

        speed = 8;

        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new PlayerMovementState(this), "PlayerMovementState");
        stateMachine.RegisterState(new PlayerThrowBirdseedState(this), "PlayerThrowBirdseedState");
        stateMachine.RegisterState(new PlayerDialogueState(this), "PlayerDialogueState");
        
    }
    private void Start()
    {
        InputManager.playerInputActions.Player.Enable();
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
    private void OnEnable()
    {
        //InputManager.playerInputActions.Player.Enable();
        InputManager.playerInputActions.Player.Quit.performed += OnQuit;
    }
    private void OnDisable()
    {
        //InputManager.playerInputActions.Player.Disable();
    }

    private void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    public void OnDialogueStarted(SignalArguments signalArgs)
    {
        stateMachine.Enter("PlayerDialogueState");
    }
    public void OnDialogueFinished()
    {
        stateMachine.Enter("PlayerMovementState");
    }


    // Ricky code

    //[HideInInspector]
    public float speed;
    [HideInInspector]
    public Vector3 movementInput;


}