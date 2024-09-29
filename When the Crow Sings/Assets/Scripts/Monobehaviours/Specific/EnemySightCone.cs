using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightCone : MonoBehaviour
{
    public EnemyController controller;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Birdseed")
        {
            controller.TriggerEntered(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Birdseed")
        {
            controller.TriggerStay(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Birdseed")
        {
            controller.TriggerExited(other);
        }
           
    }
}
