using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyState : StateMachineState
{
    PlayerController s;
    public DestroyState(PlayerController component)
    {
        s = component;
    }
}
