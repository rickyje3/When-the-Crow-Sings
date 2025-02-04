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
    public bool isCrouchingToggled = false;
    //[HideInInspector]

    //private bool _isSprinting;
    public bool isSprintingButtonHeld;
    //{
    //    set
    //    {
    //        _isSprinting = value;
    //        if (playerAnimator != null) playerAnimator.SetBool("animIsSprinting", value);

    //    }
    //    get { return _isSprinting; }
    //}
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

    public float walkSlideSpeedCorrection = 0.19f; //Used for walk(?) animation.
    public float crouchSlideSpeedCorrection = 0.19f; //Used for walk(?) animation.
    public float sprintSlideSpeedCorrection = 0.19f; //Used for walk(?) animation.

    public CharacterController characterController;

    public GameObject trajectoryLine;

    public GameSignal pauseSignalTEMP;
    public GameSignal mapSignalTEMP;
    public GameSignal historySignalTEMP;
    public GameSignal codexSignalTEMP;

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

        // I don't know why this line is here, so it's just commented out.
        //speed = 8;

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
        var direction = throwTarget.transform.position - throwPosition.transform.position;
        direction.y = 4;
        BirdseedController.Create(pfBirdseedProjectile, throwPosition, direction);
    }
    private void OnEnable()
    {
        InputManager.playerInputActions.Player.Pause.performed += OnPause;
        InputManager.playerInputActions.Player.OpenMap.performed += OnMap;
        InputManager.playerInputActions.Player.OpenHistory.performed += OnHistory;
        InputManager.playerInputActions.Player.OpenPeopleThatSoundsSOWrong.performed += OnCodex;
    }
    private void OnDisable()
    {
        InputManager.playerInputActions.Player.Pause.performed -= OnPause;
        InputManager.playerInputActions.Player.OpenMap.performed -= OnMap;
        InputManager.playerInputActions.Player.OpenHistory.performed -= OnHistory;
        InputManager.playerInputActions.Player.OpenPeopleThatSoundsSOWrong.performed -= OnCodex;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        pauseSignalTEMP.Emit();
    }
    private void OnMap(InputAction.CallbackContext context)
    {
        mapSignalTEMP.Emit();
    }
    private void OnHistory(InputAction.CallbackContext context)
    {
        historySignalTEMP.Emit();
    }
    private void OnCodex(InputAction.CallbackContext context)
    {
        codexSignalTEMP.Emit();
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
        if (!ServiceLocator.Get<DialogueManager>().isInDialogue)
        {
            stateMachine.Enter("PlayerMovementState");
        }
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