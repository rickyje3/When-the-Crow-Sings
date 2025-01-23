using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalUpdateManager : MonoBehaviour
{
    public GameSignal popupUpdateMessage;

    private void Start()
    {
        SaveDataAccess.popupUpdateMessage = popupUpdateMessage;
    }

}
