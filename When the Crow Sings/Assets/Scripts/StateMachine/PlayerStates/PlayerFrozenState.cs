using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFrozenState : StateMachineState
{
    PlayerController s;

    public PlayerFrozenState(PlayerController component)
    {
        s = component;
    }

    public override void StateEntered()
    {
        InputManager.playerInputActions.Player.Disable();
        InputManager.playerInputActions.UI.Enable();
    }
    public override void StateExited()
    {
        InputManager.playerInputActions.Player.Enable();
        InputManager.playerInputActions.UI.Disable();
    }
}
