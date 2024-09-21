using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : StateMachineState
{

    int time_remaining = 100;

    public PlayerMovementState(StateMachine stateMachine, MonoBehaviour component) : base(stateMachine, component)
    {
    }

    public override void FixedUpdate()
    {
        if (time_remaining > 0) { time_remaining -= 1; }
        else { stateMachine.Enter<PlayerState2>(); }
        
    }

    public override void StateEntered()
    {

    }

    public override void StateExited()
    {
        Debug.Log("But there was so much left to live forrr!!!!");
    }

    public override void Update(float deltaTime)
    {
        Debug.Log("I'm state 1");
    }
}
