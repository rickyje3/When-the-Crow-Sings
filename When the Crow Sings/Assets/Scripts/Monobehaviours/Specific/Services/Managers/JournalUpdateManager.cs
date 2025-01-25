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
        StartCoroutine(tempspawnafterseconds());
    }

    IEnumerator tempspawnafterseconds()
    {
        yield return new WaitForSeconds(5f);
        GameObject notification = Instantiate(notificationPrefab);
        notification.transform.parent = canvas.transform;
    }

}
