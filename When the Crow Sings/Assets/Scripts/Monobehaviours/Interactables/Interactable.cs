using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public SpriteRenderer pfInteractArrow;

    public bool autoInteract = false;

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
        else if (GetComponent<ImagePopupInteractable>())
        {
            GetComponent<ImagePopupInteractable>().EmitImagePopupSignal();
        }
        else if (GetComponent<EnemyChangeWaypointsTrigger>())
        {
            GetComponent<EnemyChangeWaypointsTrigger>().EmitChangeWaypointSignal();
        }
        else if (GetComponent<AutoSaveTrigger>())
        {
            GetComponent<AutoSaveTrigger>().AutoSave();
        }
    }

    public void OnDialogueFinished(SignalArguments args)
    {
        if ( virtualCamera != null )
        {
            virtualCamera.Priority = 10;
        }
    }

    public void setInteractableArrow(bool enabledOrDisabled)
    {
        //if (interactArrow != null)
        pfInteractArrow.enabled = enabledOrDisabled;
    }
}
