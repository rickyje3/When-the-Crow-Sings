using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class TaskStepBase : ScriptableObject
{
    public GameSignal startedSignal;
    public GameSignal finishedSignal;
    public QuickTimeEvent qte;
}
