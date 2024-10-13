using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isPlayerInTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInteractionArea>())
        {
            Debug.Log("He's here he's here he's heeereee");
            isPlayerInTrigger = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInteractionArea>())
        {
            Debug.Log("Oh never mind whew oh i hate socializing SO much.");
            isPlayerInTrigger = false;
        }
        
    }
}
