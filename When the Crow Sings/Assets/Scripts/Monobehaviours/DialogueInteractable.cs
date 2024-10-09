using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : MonoBehaviour
{
    public GameSignal signal;
    public SignalArguments signalArgs;

    private void Start()
    {
        StartCoroutine(MyMethod());
    }

    IEnumerator MyMethod()
    {
        yield return new WaitForSeconds(1.0f);
        signal.Emit(signalArgs);
    }
}
