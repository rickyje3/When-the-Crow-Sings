using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauseStun : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<GetStunned>(out GetStunned getStunned))
        {
            other.gameObject.GetComponent<EnemyController>().stateMachine.Enter("EnemyStunnedState"); //TODO: Change logic to happen in the other class.
        }
    }
}
