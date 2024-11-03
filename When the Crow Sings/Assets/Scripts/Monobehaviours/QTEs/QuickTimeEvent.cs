using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class QuickTimeEvent : MonoBehaviour
{
    abstract public void StartQTE();
    public GameSignal globalFinishedQteSignal;
}
