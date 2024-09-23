using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : StateMachineComponent
{
    private void Awake()
    {
        stateMachine = new StateMachine();
        //stateMachine.RegisterState(new PlayerMovementState(this), "PlayerMovementState");
        //stateMachine.Enter("PlayerMovementState");
    }
}
