using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IService
{
    public static PlayerInputActions playerInputActions;

    public static bool IsControllerConnected { get; private set; }

    private void Update()
    {
        // Detects if any controller is connected
        IsControllerConnected = Gamepad.current != null;
    }


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        RegisterSelfAsService();
    }

    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<InputManager>(this);
    }
}
