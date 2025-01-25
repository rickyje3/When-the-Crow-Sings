using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalUpdateManager : MonoBehaviour
{
    public GameSignal popupUpdateMessageSignal;
    public GameObject notificationPrefab;


    public Canvas canvas;

    private void Start()
    {
        SaveDataAccess.popupUpdateMessageSignal = popupUpdateMessageSignal;
    }

    public void ShowNewNotification(SignalArguments args)
    {
        GameObject notification = Instantiate(notificationPrefab);
        notification.transform.parent = canvas.transform;
    }

}
