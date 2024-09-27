using UnityEngine;

public abstract class StateMachineState//<T>// where T:MonoBehaviour //: MonoBehaviour? Interface?
{
    public virtual void Update(float deltaTime){}
    public virtual void FixedUpdate(){}//(float deltaTime){}
    public virtual void StateEntered(){}
    public virtual void StateExited(){}
    public virtual void OnEnable(){}
    public virtual void OnDisable(){}
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnTriggerExit(Collider other) { }
    
}
