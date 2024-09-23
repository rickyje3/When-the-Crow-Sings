public abstract class StateMachineState//<T>// where T:MonoBehaviour //: MonoBehaviour? Interface?
{
    public abstract void Update(float deltaTime);
    public abstract void FixedUpdate();//(float deltaTime);
    public abstract void StateEntered();
    public abstract void StateExited();
    public abstract void OnEnable();
    public abstract void OnDisable();
}
