using System;
using UnityEngine;
using UnityEngine.Events;

public class GameSignalListener : MonoBehaviour
{
    public ScriptableObjects.GameSignal gameSignal; // The SObject to listen for from outside the prefab
    public UnityEvent<SignalArguments> response; // The function that should be called within the prefab.

    private void OnEnable() { gameSignal.RegisterListener(this); }
    private void OnDisable() { gameSignal.UnregisterListener(this); }
    public void OnSignalEmitted(SignalArguments args)
    {
        response.Invoke(args);
    }

}