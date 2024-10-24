using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEInteract : MonoBehaviour
{
    public GameObject visualCue;
    public bool playerInRange;

    void Awake()
    {
        visualCue.SetActive(false);
        playerInRange = false;
    }

    public void ActivateTimingMeter()
    {
        FindObjectOfType<TimingMeter>().startQTE();
    }

    private void OnTriggerEnter(Collider QTE)
    {
        if (QTE.CompareTag("Player"))
        {
            visualCue.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider QTE)
    {
        if (QTE.CompareTag("Player"))
        {
            visualCue.SetActive(false);
            playerInRange = false;
        }
    }
}
