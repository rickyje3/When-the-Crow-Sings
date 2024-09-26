using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdseedController : StateMachineComponent
{
    public Transform pfCrowsTemp;

    private void Awake()
    {
        stateMachine = new StateMachine(this); // TODO: Figure out how to make this line "required" so it's type-safe and shows the error at compile-time.
    }
}
