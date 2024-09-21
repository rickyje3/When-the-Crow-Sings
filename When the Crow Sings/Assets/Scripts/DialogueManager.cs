using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    private float dialogueSpeed = .05f;

    //Each individual sentence spoken
    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
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

        animator.SetBool("isOpen", false);
    }
}
