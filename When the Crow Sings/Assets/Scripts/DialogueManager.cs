using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;
    public Animator DialogueAnimator;
    public Animator choiceDialogueAnimator;

    private float dialogueSpeed = .03f; // Less = faster, more = slower
    private PlayerController player;
    private bool isTyping = false;
    private bool isAfterChoice = false;
    private Dialogue currentDialogue;

    // Dialogue queues
    public Queue<string> sentences;
    public Queue<string> sentencesAfterChoice1;
    public Queue<string> sentencesAfterChoice2;
    private Queue<string> currentQueue;  // Track which sentence queue to display

    private int currentChoiceIndex = 0;  // Tracks currently selected choice

    private void Start()
    {
        sentences = new Queue<string>();
        sentencesAfterChoice1 = new Queue<string>();
        sentencesAfterChoice2 = new Queue<string>();
        currentQueue = new Queue<string>();

        player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("PlayerController not found in the scene.");
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);
        currentDialogue = dialogue;
        DialogueAnimator.SetBool("isOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        player.speed = 0; // Stop player movement during dialogue
        isAfterChoice = false;
        currentQueue = sentences;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping) return;  // Don't interrupt typing

        if (currentQueue == null || currentQueue.Count == 0)
        {
            Debug.Log("Current queue is empty or null, ending dialogue.");
            EndDialogue();
            return;
        }

        Debug.Log("Displaying next sentence. Sentences remaining: " + currentQueue.Count);

        string sentence = currentQueue.Dequeue();
        StopAllCoroutines();  // Stop any currently running typing coroutines
        StartCoroutine(TypeOutSentence(sentence));  // Start typing the next sentence
    }


    private IEnumerator TypeOutSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");

        if (sentencesAfterChoice1.Count > 0 || sentencesAfterChoice2.Count > 0)
        {
            ShowChoices();
        }
        else
        {
            Debug.Log("No choices available, closing dialogue.");
            DialogueAnimator.SetBool("isOpen", false);
            player.speed = 5; // Restore player movement after dialogue ends
        }
    }

    public void SelectChoice()
    {
        choiceDialogueAnimator.SetBool("isOpen", false);  // Close the choice display

        // Process the player's choice
        if (currentChoiceIndex == 0)
        {
            Debug.Log("Choice 1 selected");
            StartDialogueAfterChoice(1); // Now just pass the choice number
        }
        else if (currentChoiceIndex == 1)
        {
            Debug.Log("Choice 2 selected");
            StartDialogueAfterChoice(2); // Now just pass the choice number
        }
    }



    private void ShowChoices()
    {
        Debug.Log("Showing Choices");

        // Clear previous choice texts
        choice1Text.text = "";
        choice2Text.text = "";

        // Check if choices exist in the current dialogue
        if (currentDialogue.sentencesAfterChoice1.Length > 0)
        {
            choice1Text.text = currentDialogue.sentencesAfterChoice1[0]; // Display the first choice text
        }

        if (currentDialogue.sentencesAfterChoice2.Length > 0)
        {
            choice2Text.text = currentDialogue.sentencesAfterChoice2[0]; // Display the second choice text
        }

        choiceDialogueAnimator.SetBool("isOpen", true);

        // Reset choice index (default is choice 1)
        currentChoiceIndex = 0;
        HighlightCurrentChoice();
    }

        private void HighlightCurrentChoice()
    {
        // Example logic: change color to highlight selected choice
        choice1Text.color = currentChoiceIndex == 0 ? Color.black : Color.red;
        choice2Text.color = currentChoiceIndex == 1 ? Color.black : Color.red;
    }

    public void HandleChoiceSelection(bool moveDown)
    {
        // If the player moves up or down in the choices, update the selected index
        if (moveDown)
        {
            currentChoiceIndex = (currentChoiceIndex + 1) % 2;  // Switch between choice 1 and 2
        }
        else
        {
            currentChoiceIndex = (currentChoiceIndex - 1 + 2) % 2;  // Modulus to wrap around
        }

        HighlightCurrentChoice();
    }

    public void ConfirmChoice()
    {
        choiceDialogueAnimator.SetBool("isOpen", false);  // Close the choice display

        if (currentChoiceIndex == 0)
        {
            Debug.Log("Choice 1 selected. Queueing sentences for Choice 1.");
            currentQueue = sentencesAfterChoice1;  // Set the queue to sentencesAfterChoice1
        }
        else if (currentChoiceIndex == 1)
        {
            Debug.Log("Choice 2 selected. Queueing sentences for Choice 2.");
            currentQueue = sentencesAfterChoice2;  // Set the queue to sentencesAfterChoice2
        }

        isAfterChoice = true;
        Debug.Log("Starting after-choice dialogue. Sentences in queue: " + currentQueue.Count);
        DisplayNextSentence();  // Continue with the dialogue from the selected choice
    }



    public void StartDialogueAfterChoice(int choiceNumber)
    {
        if (choiceNumber == 1)
        {
            sentencesAfterChoice1.Clear();
            foreach (string sentence in currentDialogue.sentencesAfterChoice1)
            {
                sentencesAfterChoice1.Enqueue(sentence);
            }
        }
        else if (choiceNumber == 2)
        {
            sentencesAfterChoice2.Clear();
            foreach (string sentence in currentDialogue.sentencesAfterChoice2)
            {
                sentencesAfterChoice2.Enqueue(sentence);
            }
        }

        // After updating the queues, display the next sentence
        DisplayNextSentence();
    }




}



