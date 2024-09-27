using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject visualCue;
    public bool playerInRange;


    void Awake()
    {
        visualCue.SetActive(false);
        playerInRange = false;
    }

    public void ActivateDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    /*public void ActivateChoiceDialogue()
    {
        FindObjectOfType<DialogueManager>().ChoiceDialogue(dialogue);
    }

    public void ActivateDialogueAfterChoice(int choiceNumber)
    {
        FindObjectOfType<DialogueManager>().StartDialogueAfterChoice(dialogue);
    }*/

    private void OnTriggerEnter(Collider NPC)
    {
        if (NPC.CompareTag("Player"))
        {
            visualCue.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider NPC)
    {
        if (NPC.CompareTag("Player"))
        {
            visualCue.SetActive(false);
            playerInRange = false;
        }
    }
}



