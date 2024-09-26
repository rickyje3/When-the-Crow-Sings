using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauseStun : MonoBehaviour
{
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyController)) // "Stunnable" maybe for more reusability?
    //    {

    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something might get stunned now (Trigger)");
        if (other.gameObject.TryGetComponent<GetStunned>(out GetStunned getStunned))
        {
            other.gameObject.GetComponent<EnemyController>().stateMachine.Enter("EnemyStunnedState");
        }
    }
}
