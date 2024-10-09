using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IService
{
    public static PlayerInputActions playerInputActions;


    private void Awake()
    {
        Debug.Log("anything??");
        playerInputActions = new PlayerInputActions();
        Debug.Log(playerInputActions+ " is the thing");
        RegisterSelfAsService();
        
    }
    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<InputManager>(this);
    }
}
