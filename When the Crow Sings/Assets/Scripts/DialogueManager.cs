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

    private float dialogueSpeed = .02f; // Less = faster, more = slower
    private PlayerController player;
    private bool isTyping = false;
    private bool isAfterChoice = false;
    private Dialogue currentDialogue;
    public bool choicesShown;
    public bool inDialogue;
    public bool noChoices;

    // Dialogue queues
    public Queue<string> sentences;
    public Queue<string> choices;
    public Queue<string> sentencesAfterChoice1;
    public Queue<string> sentencesAfterChoice2;
    private Queue<string> currentQueue;  // Track which sentence queue to display

    private int currentChoiceIndex = 0;  // Tracks currently selected choice

    private void Start()
    {
        sentences = new Queue<string>();
        choices = new Queue<string>();
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
        currentDialogue = dialogue; // Set the current dialogue

        DialogueAnimator.SetBool("isOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            Debug.Log("Enqueued sentence: " + sentence);
        }

        // Log after populating sentencesAfterChoice
        Debug.Log("AfterChoice1 count: " + dialogue.sentencesAfterChoice1.Length);
        Debug.Log("AfterChoice2 count: " + dialogue.sentencesAfterChoice2.Length);

        currentQueue = sentences;

        player.speed = 0; // Stop player movement during dialogue
        isAfterChoice = false;
        inDialogue = true;

        DisplayNextSentence(); // Ensure this is called after the queue is set up
    }





    public void DisplayNextSentence()
    {
        if (isTyping) return;  // Don't interrupt typing

        if (currentQueue.Count == 0)
        {
            EndDialogue();
            return;
        }


        Debug.Log("Displaying next sentence");

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

        if (noChoices)
        {
            Debug.Log("No choices available, closing dialogue.");
            DialogueAnimator.SetBool("isOpen", false);
            player.speed = 10; // Restore player movement after dialogue ends
            inDialogue = false;
        }
        else
        {
            if (!isAfterChoice)
            {
                ShowChoices(); // Show choices if there are any
            }
            else if (isAfterChoice)
            {
                Debug.Log("No choices available, closing dialogue.");
                DialogueAnimator.SetBool("isOpen", false);
                player.speed = 10; // Restore player movement after dialogue ends
                inDialogue = false;
            }
        }
    }


    public void SelectChoice()
    {
        choiceDialogueAnimator.SetBool("isOpen", false); // Close the choice display

        // Process the player's choice
        if (currentChoiceIndex == 0)
        {
            Debug.Log("Choice 1 selected");
            StartDialogueAfterChoice(1); // Pass the choice number
        }
        else if (currentChoiceIndex == 1)
        {
            Debug.Log("Choice 2 selected");
            StartDialogueAfterChoice(2); // Pass the choice number
        }
    }


    private void ShowChoices()
    {
        Debug.Log("Showing Choices");
        choicesShown = true;

        // Clear previous choice texts
        choice1Text.text = "";
        choice2Text.text = "";

        // Check if choices exist in the current dialogue
        if (currentDialogue.choices1.Length > 0)
        {
            choice1Text.text = currentDialogue.choices1[0]; // Display the first choice text
        }

        if (currentDialogue.choices2.Length > 0)
        {
            choice2Text.text = currentDialogue.choices2[0]; // Display the second choice text
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
        choicesShown = false;

        // Process the player's choice
        if (currentChoiceIndex == 1)
        {
            Debug.Log("Choice 1 selected");
            currentQueue = sentencesAfterChoice1;
            StartDialogueAfterChoice(1);
        }
        else if (currentChoiceIndex == 0)
        {
            Debug.Log("Choice 2 selected");
            currentQueue = sentencesAfterChoice2;
            StartDialogueAfterChoice(2);
        }

        isAfterChoice = true;
        DisplayNextSentence();  // Ensure this is called to show the next sentence
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
            currentQueue = sentencesAfterChoice1; // Set currentQueue to after-choice sentences
        }
        else if (choiceNumber == 2)
        {
            sentencesAfterChoice2.Clear();
            foreach (string sentence in currentDialogue.sentencesAfterChoice2)
            {
                sentencesAfterChoice2.Enqueue(sentence);
            }
            currentQueue = sentencesAfterChoice2; // Set currentQueue to after-choice sentences
        }

        isAfterChoice = true;
        DisplayNextSentence(); // After updating the queues, display the next sentence
    }

}



