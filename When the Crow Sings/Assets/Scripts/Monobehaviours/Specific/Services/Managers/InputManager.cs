using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IService
{
    public static PlayerInputActions playerInputActions;


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
