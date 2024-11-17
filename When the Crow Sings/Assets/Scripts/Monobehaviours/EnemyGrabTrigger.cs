using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrabTrigger : MonoBehaviour
{
    public GameSignal enemyCaughtPlayerSignal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            enemyCaughtPlayerSignal.Emit();
        }
    }
}
