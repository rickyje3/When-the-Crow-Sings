using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ExampleService : MonoBehaviour, IService
{
    private void Awake()
    {
        register_self();
    }

    public void register_self()
    {
        ServiceLocator.Register<ExampleService>(this);
    }

    public void DoExampleThing()
    {
        Debug.Log("I did an example thing!");
    }
}
