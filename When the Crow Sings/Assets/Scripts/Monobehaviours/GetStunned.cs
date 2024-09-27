using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetStunned : MonoBehaviour
{

    
    private void OnValidate()
    {
        ValidateComponents();
    }

    private void ValidateComponents()
    {
        bool hasRequiredComponent = false;
        List<System.Type> requiredComponents = new List<System.Type> { typeof(EnemyController) };


        foreach (System.Type requiredComponent in requiredComponents)
        {
            if (GetComponent(requiredComponent) != null)
            {
                hasRequiredComponent = true;
            }
        }

        if (!hasRequiredComponent)
        {
            Debug.LogError("Warning! GetStunned requires ONE of the following components on " + gameObject.name + ": [EnemyController]");
        }
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.TryGetComponent<CauseStun>(out CauseStun causeStun))
    //    {
    //        GetComponent<EnemyController>().stateMachine.Enter("EnemyStunnedState");
    //        Debug.Log("The enemy has been stunned.");
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Going to try to stun now:");

    //    if (other.gameObject.TryGetComponent<CauseStun>(out CauseStun causeStun))
    //    {
    //        GetComponent<EnemyController>().stateMachine.Enter("EnemyStunnedState");
    //        Debug.Log("The enemy has been stunned.");
    //    }
    //    else
    //    {
    //        Debug.Log("The enemy has not been stunned.");
    //    }
    //}


}
