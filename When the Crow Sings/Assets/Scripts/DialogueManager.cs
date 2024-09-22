using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private TextMeshProUGUI[] choicesText;
    public Animator DialogueAnimator;
    public Animator choiceDialogueAnimator;
    private float dialogueSpeed = .05f;
    public PlayerController player;
    //Each individual sentence spoken
    public Queue<string> sentences;
    public Queue<string> choices;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        player = FindObjectOfType<PlayerController>(); 

        if (player == null)
        {
            Debug.LogError("PlayerController not found in the scene.");
        }

    }


    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        DialogueAnimator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        player.speed = 0;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //String that stores the dequeue
        string sentence = sentences.Dequeue();
        //Then loads the next sentence
        dialogueText.text = sentence;
        Debug.Log(sentence);

        StopAllCoroutines();
        StartCoroutine(TypeOutSentence(sentence));
    }

    private IEnumerator TypeOutSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            //amount of wait time between each letter 
            yield return new WaitForSeconds(dialogueSpeed);
        }

    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");

        if (choices.Count > 0)
        {

        }
        else if (choices.Count == 0)
        {
            DialogueAnimator.SetBool("isOpen", false);
            player.speed = 5;
        }
    }

    public void ChoiceDialogue(Dialogue dialogue)
    {
        choices = new Queue<string>();
        choiceDialogueAnimator.SetBool("isOpen", true);
    }
}
