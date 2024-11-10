using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    // Where we keep track of quest stuff, as well as the interface to handle them.


    //public List<TaskAsset> allTasks;
    
    public QTE_UI_Holder qteUiHolder;


    public void StartQTE(int whichQTETempVarJustAsAnExapleDontReadTooMuchIntoIt)
    {

    }

    public void OnQTEStartRequested(SignalArguments signalArgs)
    {
        QuickTimeEvent qteToLoad = (QuickTimeEvent)signalArgs.objectArgs[0];
        qteUiHolder.LoadQte(qteToLoad);
    }

    public void OnQTEStarted(SignalArguments signalArgs)
    {

    }

    public void OnQteFinished(SignalArguments signalArgs)
    {
        qteUiHolder.DestroyQte();
    }
}
