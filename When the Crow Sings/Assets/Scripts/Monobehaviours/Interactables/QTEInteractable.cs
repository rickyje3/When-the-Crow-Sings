using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEInteractable : MonoBehaviour
{
    public QuickTimeEvent qte;

    [Header("Built-In")]
    public GameSignal globalQteSignal;

    private SignalArguments signalArgs;
    private void Awake()
    {
        signalArgs = new SignalArguments();
        signalArgs.objectArgs.Add(qte);
        // TODO maybe task/step???
    }

    public void EmitStartQteSignal()
    {
        globalQteSignal.Emit(signalArgs);
    }

    //public GameObject visualCue;
    //public bool playerInRange;

    //void Awake()
    //{
    //    visualCue.SetActive(false);
    //    playerInRange = false;
    //}

    //public void ActivateTimingMeter()
    //{
    //    FindObjectOfType<TimingMeter>().startQTE();
    //}

    //private void OnTriggerEnter(Collider QTE)
    //{
    //    if (QTE.CompareTag("Player"))
    //    {
    //        visualCue.SetActive(true);
    //        playerInRange = true;
    //    }
    //}

    //private void OnTriggerExit(Collider QTE)
    //{
    //    if (QTE.CompareTag("Player"))
    //    {
    //        visualCue.SetActive(false);
    //        playerInRange = false;
    //    }
    //}
}
