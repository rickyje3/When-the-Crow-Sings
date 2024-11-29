using ScriptableObjects;
using System.Collections;
using UnityEngine;

public class CrowTarget : MonoBehaviour
{
    public float SecondsToAttractCrows = 5.0f;
    [HideInInspector]
    public bool isActiveTarget = false;
    

    public GameObject visualDebug;

    public GameSignal enabledSignal;
    public GameSignal disabledSignal;

    public void SetActiveTarget()
    {
        isDisableAfterTimeRunning = false;
        isActiveTarget = true;
        enabledSignal.Emit();
        visualDebug.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BirdBrain>() != null)
        {
            //other.GetComponent<BirdBrain>().stateMachine.Enter("CrowPeckState");
            if (!isDisableAfterTimeRunning) StartCoroutine(DisableAfterTime());
        }
    }

    bool isDisableAfterTimeRunning = true;
    IEnumerator DisableAfterTime()
    {
        isDisableAfterTimeRunning = true;
        yield return new WaitForSeconds(SecondsToAttractCrows);
        isActiveTarget = false;

        Destroy(ServiceLocator.Get<GameManager>().activeBirdseed.gameObject);
        ServiceLocator.Get<GameManager>().activeBirdseed = null;
        disabledSignal.Emit();
        
        visualDebug.SetActive(false);
        isDisableAfterTimeRunning = false;
    }
}