using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void DoInteraction()
    {
        if (GetComponent<DialogueInteractable>())
        {
            GetComponent<DialogueInteractable>().EmitStartDialogueSignal();
        }
    }
}
