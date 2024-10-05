using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour, IService
{
    private void Awake()
    {
        RegisterSelfAsService();
    }
    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<GameStateManager>(this);
    }
}
