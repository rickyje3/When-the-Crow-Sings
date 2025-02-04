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
        // Would have been better to just use some polymorphism, ah well at least it's clear enough.
        if (GetComponent<DialogueInteractable>())
        {
            GetComponent<DialogueInteractable>().EmitStartDialogueSignal();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Interact);
        }
        else if (GetComponent<QTEInteractable>())
        {
            GetComponent<QTEInteractable>().EmitStartQteSignal();
        }
        else if (GetComponent<CameraInteractable>())
        {
            GetComponent<CameraInteractable>().EmitCameraInteractionSignal();
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
        EndInteraction();
    }

    void EndInteraction()
    {
        if (virtualCamera != null)
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
