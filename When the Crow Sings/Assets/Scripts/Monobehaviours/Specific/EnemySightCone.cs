using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightCone : MonoBehaviour
{
    public EnemyController controller;

    private void OnTriggerEnter(Collider other)
    {
        controller.TriggerEntered(other);
    }
    private void OnTriggerExit(Collider other)
    {
        controller.TriggerExited(other);
    }
}
