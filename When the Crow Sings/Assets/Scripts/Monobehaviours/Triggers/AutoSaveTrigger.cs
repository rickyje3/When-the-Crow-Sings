using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaveTrigger : MonoBehaviour
{
    public string flagToFlip = "thisShouldMatchTheDynamicEnable";
    public void AutoSave()
    {
        Debug.Log("Autosaving!");
        SaveDataAccess.WriteDataToDisk();
        SaveDataAccess.SetFlag(flagToFlip,true);
        
    }
}
