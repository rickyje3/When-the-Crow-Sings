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


    SuccessAndFailValues currentQTESuccessAndFailValues = null;
    public void OnQTEStartRequested(SignalArguments signalArgs)
    {
        QuickTimeEvent qteToLoad = (QuickTimeEvent)signalArgs.objectArgs[0];
        currentQTESuccessAndFailValues = (SuccessAndFailValues)signalArgs.objectArgs[1];

        qteUiHolder.LoadQte(qteToLoad);
    }

    public void OnQTEStarted(SignalArguments signalArgs)
    {

    }

    public void OnQteFinished(SignalArguments signalArgs)
    {
        qteUiHolder.DestroyQte();

        if (signalArgs.boolArgs[0])
        {
            if (currentQTESuccessAndFailValues.success != null)
            {
                foreach (KeyValuePair<string, bool> i in currentQTESuccessAndFailValues.success)
                {
                    SaveDataAccess.SetFlag(i.Key, i.Value);
                }
            }
        }
        else
        {
            if (currentQTESuccessAndFailValues.fail != null)
            {
                foreach (KeyValuePair<string, bool> i in currentQTESuccessAndFailValues.fail)
                {
                    SaveDataAccess.SetFlag(i.Key, i.Value);
                }
            }
        }
        currentQTESuccessAndFailValues = null;
        
    }
}
