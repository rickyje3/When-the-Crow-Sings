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

    private bool _isSprinting;
    public bool isSprinting
    {
        set
        {
            _isSprinting = value;
            if (playerAnimator != null) playerAnimator.SetBool("animIsSprinting", value);

        }
        get { return _isSprinting; }
    }
    [HideInInspector]
    public float gravity = -9.81f;
    [HideInInspector]
    public float gravityMultiplier = 3f;
    [HideInInspector]
    public float gravityVelocity;

    public float maxWalkSpeed;
    public float minWalkSpeed;
    public float minSprintSpeed;
    public float maxSprintSpeed;
    public float minCrouchSpeed;
    public float maxCrouchSpeed;

    public float slideSpeedCorrection = 0.19f; // Used for walk(?) animation.
    public CharacterController characterController;

    public GameSignal pauseSignalTEMP;

    public void ApplyGravity(float deltaTime)
    {
        // Apply gravity to velocity
        gravityVelocity += gravity * gravityMultiplier * deltaTime;

        if (characterController.isGrounded && gravityVelocity < 0)
        {
            gravityVelocity = 0; // Reset vertical velocity
        }
    }

    private void Awake()
    {
        RegisterSelfAsService();

        characterController = GetComponent<CharacterController>();

        speed = 8;

        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new PlayerFrozenState(this), "PlayerFrozenState");
        stateMachine.RegisterState(new PlayerMovementState(this), "PlayerMovementState");
        stateMachine.RegisterState(new PlayerThrowBirdseedState(this), "PlayerThrowBirdseedState");
    }
    private void Start()
    {
        InputManager.playerInputActions.Player.Enable();
        stateMachine.Enter("PlayerFrozenState");
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
        pauseSignalTEMP.Emit();
    }

    public void OnDialogueStarted(SignalArguments signalArgs)
    {
        stateMachine.Enter("PlayerFrozenState");
    }
    public void OnDialogueFinished()
    {
        stateMachine.Enter("PlayerMovementState");
    }

    public void OnFullyLoadFinished(SignalArguments args)
    {
        stateMachine.Enter("PlayerMovementState");
    }

    public void OnAnimationFinished(SignalArguments args)
    {
        if (args.stringArgs[0] == "Throw")
        {
            stateMachine.Enter("PlayerMovementState");
        }
    }


    // Ricky code

    //[HideInInspector]
    public float speed;
    [HideInInspector]
    public Vector3 movementInput;
    [HideInInspector]
    public float acceleration = 5f;

}