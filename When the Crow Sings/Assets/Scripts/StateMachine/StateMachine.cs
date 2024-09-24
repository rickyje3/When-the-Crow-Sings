using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public StateMachineState currentState;
    StateMachineState previousState;
    public Dictionary<Type,object> states = new Dictionary<Type,object>();
    //StateMachineState[] states
    //{
    //    get { return GetComponents<StateMachineState>(); } // ???
    //}

    //private MonoBehaviour component;
    public StateMachine()//MonoBehaviour monoBehaviour)
    {

        // TODO: Figure out how to get Update and FixedUpdate to be hooked up via the constructor.
    }

    // Callbacks
    public void Update(float deltaTime)
    {
        currentState.Update(deltaTime);
    }
    public void FixedUpdate()
    {
        currentState.FixedUpdate();
    }



    public void Enter(StateMachineState new_state) 
    {
        if (new_state == currentState) { return;}
        if(currentState != null) { currentState.StateExited(); }
        previousState = currentState;
        currentState = new_state;
        currentState.StateEntered();
    }

    //public void RegisterState<T>(T state) where T : StateMachineState
    //{
    //    states[typeof(T)] = state;
    //}
    public void RegisterState(StateMachineState state)
    {
        states[state.GetType()] = state;
    }

    public void Enter<T>() where T : StateMachineState
    {
       
        T new_state = (T)states[typeof(T)];
        // turn string to state
        Enter(new_state);
    }
    public void EnterPrevious()
    {
        Enter(previousState);
    }

   


    // Unused
    private void _enableState(StateMachineState targetState)
    {

    }
    private void _disableState(StateMachineState targetState)
    {

    }

    //public void Enter<T>(StateMachineState new_state, List<T> args) // ?????????????????????????
    //{

    //}
}
