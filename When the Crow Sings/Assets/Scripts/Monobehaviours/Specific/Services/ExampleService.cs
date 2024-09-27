using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ExampleService : MonoBehaviour, IService
{
    private void Awake()
    {
        RegisterSelfAsService();
    }

    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<ExampleService>(this);
    }

    public void DoExampleThing()
    {
        Debug.Log("I did an example thing!");
    }
}
