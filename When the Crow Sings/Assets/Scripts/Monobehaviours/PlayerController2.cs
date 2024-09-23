using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController2 : MonoBehaviour
{
    private StateMachine stateMachine;
    private PlayerMovementState playerState1;
    private PlayerState2 playerState2;

    public float speed = 5.0f;
    [HideInInspector]
    public Vector3 movementInput;
    public List<DialogueInteract> dialogueInteractables = new List<DialogueInteract>();

    private void Awake()
    {
        stateMachine = new StateMachine();

        playerState1 = new PlayerMovementState(stateMachine, this);
        playerState2 = new PlayerState2(stateMachine,this);

        stateMachine.Enter<PlayerMovementState>();
    }

    private void Update() => stateMachine.Update(Time.deltaTime);
    private void FixedUpdate() => stateMachine.FixedUpdate();
    //private void OnEnable() => stateMachine.currentState.OnEnable(); // Maybe have this logic in EnterState instead?
    //private void OnDisable() => stateMachine.currentState.OnEnable();


    //private void Update()
    //{
    //    stateMachine.Update(Time.deltaTime);
    //}
}




//public abstract class StateMachineMonoBehaviour : MonoBehaviour
//{
//    private StateMachine stateMachine;
//    private void Update()
//    {
//        stateMachine.Update(Time.deltaTime);
//    }

//    public StateMachineMonoBehaviour(StateMachine stateMachine)
//    {
//        this.stateMachine = stateMachine;
//    }
//}

//public class MyStateMachineMB : StateMachineMonoBehaviour
//{
//    public MyStateMachineMB(StateMachine stateMachine) : base(stateMachine)
//    {
//    }

//}