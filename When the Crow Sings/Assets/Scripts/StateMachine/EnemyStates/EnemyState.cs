using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyState : StateMachineState
{
    public EnemyController s;
    protected EnemyState(EnemyController component)
    {
        s = component;
    }
}
