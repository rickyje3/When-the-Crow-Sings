using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateMachineState//<T>// where T:MonoBehaviour //: MonoBehaviour? Interface?
{
    public StateMachine stateMachine;
    public MonoBehaviour component;

    // Entered Signal?
    // Exited Signal?

    public abstract void Update(float deltaTime);
    public abstract void FixedUpdate();//(float deltaTime);
    public abstract void StateEntered();
    public abstract void StateExited();
    public abstract void OnEnable();
    public abstract void OnDisable();

    protected StateMachineState(StateMachine stateMachine, MonoBehaviour myComponent)
    {
        this.stateMachine = stateMachine;
        this.component = myComponent;

        Type type = this.GetType();

        //stateMachine.RegisterState<type>(this);
        stateMachine.RegisterState(this);
    }
}
