using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadZone : MonoBehaviour
{
    public GameSignal startLoadSignal;
    public int targetIndexTEMP = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInteractionArea>() != null)
        {
            Debug.Log("Going to try to load!");
            SignalArguments args = new SignalArguments();
            args.intArgs.Add(targetIndexTEMP);
            startLoadSignal.Emit(args);
        }
    }
}
