using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    StateMachineState currentState;
    StateMachineState previousState;
    //StateMachineState[] states
    //{
    //    get { return GetComponents<StateMachineState>(); } // ???
    //}

    public void Enter(StateMachineState new_state) 
    {
        if (new_state == currentState) { return;}
    }
    
    public void Enter(string new_state)
    {
        // turn string to state
        Enter(new_state);
    }
    public void EnterPrevious()
    {
        Enter(previousState);
    }

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
