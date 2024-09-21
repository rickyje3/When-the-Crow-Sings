using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController2 : MonoBehaviour
{
    private StateMachine stateMachine;
    private PlayerMovementState playerState1;
    private PlayerState2 playerState2;

    private void Awake()
    {
        stateMachine = new StateMachine();

        playerState1 = new PlayerMovementState(stateMachine, this);
        playerState2 = new PlayerState2(stateMachine,this);

        //stateMachine.RegisterState<PlayerMovementState>(playerState1);
        //stateMachine.RegisterState<PlayerState2>(playerState2);

        stateMachine.Enter<PlayerMovementState>();
    }

    private void Update() => stateMachine.Update(Time.deltaTime);
    private void FixedUpdate() => stateMachine.FixedUpdate();

}




