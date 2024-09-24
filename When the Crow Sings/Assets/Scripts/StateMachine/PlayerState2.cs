using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState2 : StateMachineState
{
    public PlayerState2(StateMachine stateMachine, MonoBehaviour component) : base(stateMachine, component)
    {
    }

    public override void FixedUpdate()
    {

    }

    public override void OnDisable()
    {
        
    }

    public override void OnEnable()
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
