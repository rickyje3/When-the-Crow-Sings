using UnityEngine;

public abstract class StateMachineState //: MonoBehaviour? Interface?
{
    public StateMachine stateMachine;

    // Entered Signal?
    // Exited Signal?

    public abstract void Update(float deltaTime);
    public abstract void FixedUpdate();//(float deltaTime);
    public abstract void StateEntered();
    public abstract void StateExited();
}
