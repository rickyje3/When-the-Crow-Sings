using ScriptableObjects;
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
    //[HideInInspector]
    public bool isSprinting = false;
    [HideInInspector]
    public float gravity = -9.81f;
    [HideInInspector]
    public float gravityMultiplier = 3f;
    [HideInInspector]
    public float velocity;
    [HideInInspector] public float maxWalkSpeed = 5f;
    [HideInInspector] public float minWalkClamp = .5f;
    public float slideSpeedCorrection = 0.19f;
    public CharacterController characterController;
    public Canvas pauseCanvas;

    public GameSignal pauseSignalTEMP;

    private void Awake()
    {
        RegisterSelfAsService();

        characterController = GetComponent<CharacterController>();

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

    private void OnDestroy()
    {
        stateMachine.RegisterState(new DestroyState(this), "DestroyState");
        stateMachine.Enter("DestroyState");
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
        InputManager.playerInputActions.Player.Pause.performed += OnPause;
    }
    private void OnDisable()
    {
        InputManager.playerInputActions.Player.Pause.performed -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        pauseCanvas.gameObject.SetActive(true);
        pauseSignalTEMP.Emit();
        Debug.Log("Paused");
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
    [HideInInspector]
    public float acceleration = 5f;

}