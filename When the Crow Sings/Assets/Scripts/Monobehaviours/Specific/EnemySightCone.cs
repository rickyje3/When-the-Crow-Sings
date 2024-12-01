using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightCone : MonoBehaviour
{
    public EnemyController controller;
    [HideInInspector] public bool playerInSightCone = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) playerInSightCone=true;
        //if (other.gameObject.tag == "Player" || other.gameObject.tag == "Birdseed")
        //{
        //    controller.SightConeTriggerEntered(other);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.CompareTag("Player")) playerInSightCone = true;
        //else playerInSightCone = false;

        //if (other.gameObject.tag == "Player" || other.gameObject.tag == "Birdseed")
        //{
        //    controller.SightConeTriggerStay(other);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) playerInSightCone = false;
        //if (other.gameObject.tag == "Player" || other.gameObject.tag == "Birdseed")
        //{
        //    controller.SightConeTriggerExited(other);
        //}

    }

    private void Awake()
    {
        GetComponent<MeshRenderer>().enabled = DebugManager.showCollidersAndTriggers;
    }
}
