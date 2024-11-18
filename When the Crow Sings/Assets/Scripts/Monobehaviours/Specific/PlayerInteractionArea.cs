using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionArea : MonoBehaviour
{
    public List<Interactable> interactablesInRange = new List<Interactable>();
    public bool canInteract
    {
        get
        {
            return interactablesInRange.Count > 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            Interactable interactable = other.GetComponent<Interactable>();
            //Debug.Log("He's here he's here he's heeereee");
            interactablesInRange.Add(interactable);
            interactable.setInteractableArrow(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            Interactable interactable = other.GetComponent<Interactable>();
            //Debug.Log("Oh never mind whew oh i hate socializing SO much.");
            interactablesInRange.Remove(interactable);
            interactable.setInteractableArrow(false);
        }

    }

    private void Update()
    {
        List<Interactable> interactablesToRemove = new List<Interactable>();
        foreach (Interactable i in interactablesInRange)
        {
            if (!i.gameObject.activeInHierarchy)
            {
                interactablesToRemove.Add(i);
            }
        }
        foreach (Interactable i in interactablesToRemove)
        {
            interactablesInRange.Remove(i);
        }
        
    }

    private void Start()
    {
        InputManager.playerInputActions.Player.Interact.performed += OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            interactablesInRange[0].DoInteraction();
        }
    }
}
