using UnityEngine;

public class PlayerState2 : StateMachineState
{
    PlayerController s;
    public PlayerState2(PlayerController component, StateMachine machine)
    {
        s = component;
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
