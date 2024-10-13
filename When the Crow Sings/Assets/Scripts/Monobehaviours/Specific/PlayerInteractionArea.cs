using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionArea : MonoBehaviour
{
    public List<Interactable> interactablesInRange = new List<Interactable>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            Debug.Log("He's here he's here he's heeereee");
            interactablesInRange.Add(other.GetComponent<Interactable>());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            Debug.Log("Oh never mind whew oh i hate socializing SO much.");
            interactablesInRange.Remove(other.GetComponent<Interactable>());
        }

    }

    private void Start()
    {
        InputManager.playerInputActions.Player.Interact.performed += OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (interactablesInRange.Count > 0)
        {
            interactablesInRange[0].DoInteraction();
        }
    }
}
