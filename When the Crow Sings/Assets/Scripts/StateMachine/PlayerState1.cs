using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState1 : StateMachineState
{

    int time_remaining = 100;
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
        Debug.Log("No wait I had so much to live fooooor!!!");
    }

    public override void Update(float deltaTime)
    {
        Debug.Log("State 1 is updating!");
    }
}
