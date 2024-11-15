using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public SpriteRenderer interactArrow;


    public void DoInteraction()
    {
        if (virtualCamera != null)
        {
            virtualCamera.Priority = 100;
        }

        if (GetComponent<DialogueInteractable>())
        {
            GetComponent<DialogueInteractable>().EmitStartDialogueSignal();
        }
        else if (GetComponent<QTEInteractable>())
        {
            GetComponent<QTEInteractable>().EmitStartQteSignal();
        }
        /*else if getcomponent qte interactable
        {
            do thing;
        }*/
    }

    public void OnDialogueFinished(SignalArguments args)
    {
        if ( virtualCamera != null )
        {
            virtualCamera.Priority = 10;
        }
    }
}
