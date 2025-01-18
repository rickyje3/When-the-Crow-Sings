using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public GameObject virtualCameraHolder;

    [HideInInspector]
    public List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();

    private void Start()
    {
        virtualCameras = virtualCameraHolder.GetComponentsInChildren<CinemachineVirtualCamera>().ToList();
    }


    int loop = 0;
    IEnumerator simpleProgress()
    {
        foreach (CinemachineVirtualCamera i in virtualCameras)
        {
            i.Priority = 0;
        }
        virtualCameras[loop].Priority = 10;

        yield return new WaitForSeconds(1f);


        if (loop < virtualCameras.Count - 1)
        StartCoroutine(simpleProgress());
    }

}
