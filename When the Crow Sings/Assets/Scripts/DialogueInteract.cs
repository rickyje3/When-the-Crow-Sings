using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    public Dialogue dialogue;

    public void ActivateDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
