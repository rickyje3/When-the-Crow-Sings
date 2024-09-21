using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateMachineState //: MonoBehaviour? Interface?
{
    public StateMachine stateMachine;
    public MonoBehaviour component; 

    // Entered Signal?
    // Exited Signal?

    public abstract void Update(float deltaTime);
    public abstract void FixedUpdate();//(float deltaTime);
    public abstract void StateEntered();
    public abstract void StateExited();

    protected StateMachineState(StateMachine stateMachine, MonoBehaviour component)
    {
        this.stateMachine = stateMachine;
        this.component = component;

        Type type = this.GetType();

        //stateMachine.RegisterState<type>(this);
        stateMachine.RegisterState(this);
    }
}
