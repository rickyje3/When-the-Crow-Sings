using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueInteract : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject visualCue;
    public bool playerInRange;
    public Sprite NPCSprite; //If there is no NPC image for this interaction leave it empty
    public Image NPCImage; //This always needs to be attached whether there is a NPC sprite or not
    public float targetAlpha;
    [HideInInspector] public DialogueManager dialogueManager;


    

    void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        visualCue.SetActive(false);
        playerInRange = false;
    }

    public void ActivateDialogue()
    {
        if (NPCSprite != null)
        {
            targetAlpha = 255;
            NPCImage.sprite = NPCSprite;
        }
        else if (NPCSprite == null)
        {
            targetAlpha = 0;
        }

        // Get the current color of the image
        Color NPCImageAlpha = NPCImage.color;

        // Change the alpha value
        NPCImageAlpha.a = targetAlpha;

        // Assign the updated color back to the image
        NPCImage.color = NPCImageAlpha;

        if (dialogue != null)
        {
            if (dialogue.choices1 == null || dialogue.choices1.Length == 00 &&
                dialogue.choices2 == null || dialogue.choices2.Length == 00)
            {
                dialogueManager.noChoices = true;
                Debug.Log("No choices");
            }
            else
            {
                dialogueManager.noChoices = false;
                Debug.Log("Else");
            }
            dialogueManager.StartDialogue(dialogue);
        }
    }

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



