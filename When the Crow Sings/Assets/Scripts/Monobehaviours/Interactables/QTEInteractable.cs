using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEInteractable : MonoBehaviour
{
    public QuickTimeEvent qte;

    [Header("Built-In")]
    public GameSignal globalQteSignal;

    public SerializableDictionary successDict;
    public SerializableDictionary failureDict;

    private SignalArguments signalArgs;
    private void Awake()
    {
        signalArgs = new SignalArguments();
        signalArgs.objectArgs.Add(qte);

        if (successDict.elements.Count == 0) throw new System.Exception("QTE does not have success dict!");
        if (failureDict.elements.Count == 0) throw new System.Exception("QTE does not have failure dict!");
        successDict.ValidateNotBlank();
        failureDict.ValidateNotBlank();

        SuccessAndFailValues successAndFailValues = new SuccessAndFailValues();
        foreach (SerializableDictionaryElement i in successDict.elements)
        {
            successAndFailValues.success[i.key] = i.value;
        }
        foreach (SerializableDictionaryElement i in failureDict.elements)
        {
            successAndFailValues.fail[i.key] = i.value;
        }
        signalArgs.objectArgs.Add(successAndFailValues);
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
