using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : MonoBehaviour
{
    public GameSignal globalDialogueSignal;
    public string startingTitle = "";
    public DialogueResource dialogueResource;
    private SignalArguments signalArgs;

    private void Awake()
    {
        signalArgs = new SignalArguments();
        signalArgs.stringArgs.Add(startingTitle);
        signalArgs.objectArgs.Add(dialogueResource);
    }

    private void Start()
    {
        //StartCoroutine(MyMethod());

    }

    //IEnumerator MyMethod()
    //{
    //    yield return new WaitForSeconds(1.0f);
    //    EmitStartDialogueSignal();
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    EmitStartDialogueSignal();
    //}

    public void EmitStartDialogueSignal()
    {
        globalDialogueSignal.Emit(signalArgs);
    }
}
