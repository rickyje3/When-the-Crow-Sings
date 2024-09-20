using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState2 : StateMachineState
{
    public override void FixedUpdate()
    {

    }

    public override void StateEntered()
    {
        Debug.Log("I have been born.");
    }

    public override void StateExited()
    {
       
    }

    public override void Update(float deltaTime)
    {
        Debug.Log("State 2 is updating!");
    }
}
