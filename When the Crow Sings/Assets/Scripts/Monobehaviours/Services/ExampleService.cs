using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ExampleService : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.Register<ExampleService>(this);
    }
    public void DoExampleThing()
    {
        Debug.Log("I did an example thing!");
    }
}
