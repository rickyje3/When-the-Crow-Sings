using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour, IService
{
    public DialogueResource dialogueResource;
    public GameSignal[] signalsDialogueCanUse;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private TextMeshProUGUI nameText;

    public float textSpeed = .05f;
    public float pauseMultiplier = 10f;


    private void Awake()
    {
        RegisterSelfAsService();
        StartDialogueTESTING();
    }
    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<DialogueManager>(this);
        
    }

    private void Start()
    {
        DialogueParser parser = new DialogueParser(dialogueResource);
        foreach (DialogueBase i in dialogueResource.dialogueLines)
        {
            Debug.Log(i);
        }
    }




    void StartDialogueTESTING()
    {
        
        StartCoroutine(TypeText(dialogueText, dialogueText.text));
    }


    IEnumerator TypeText(TextMeshProUGUI textMesh, string text)
    {
        dialogueText.maxVisibleCharacters = 0;
        while (textMesh.maxVisibleCharacters < textMesh.text.Length)
        {
            float pauseBetweenChars = textSpeed;
            char character = textMesh.text[Mathf.Clamp(textMesh.maxVisibleCharacters - 1,0,textMesh.text.Length)];
            foreach (char i in ".!?")
            {
                if (character == i)
                {
                    pauseBetweenChars *= pauseMultiplier;
                }
            }
            yield return new WaitForSeconds(pauseBetweenChars);
            textMesh.maxVisibleCharacters += 1;
        }
    }


    void GetNextLine()
    {
        // SUDO: if goto, empty, or conditional, get/"do"(?) the next-next line automatically. Otherwise, wait for input/signal/whatever.
        // only exception is Choices, which need to be "got" on "finished typing" not "on input").
    }

    void DoConditionalDialogueLogic(DialogueCondition dialogueCondition)
    {
        switch (dialogueCondition.logicType)
        {
            case DialogueCondition.LogicType.IF:
                switch (dialogueCondition.operatorType)
                {
                    case DialogueCondition.OperatorType.EQUAL_TO:

                        break;

                    case DialogueCondition.OperatorType.GREATER_THAN:

                        break;

                    case DialogueCondition.OperatorType.GREATER_THAN_OR_EQUAL_TO:

                        break;

                    case DialogueCondition.OperatorType.LESS_THAN:

                        break;

                    case DialogueCondition.OperatorType.LESS_THAN_OR_EQUAL_TO:

                        break;

                    case DialogueCondition.OperatorType.NOT_EQUAL_TO:

                        break;
                }
                break;
                
            case DialogueCondition.LogicType.ELIF:
                break;
            case DialogueCondition.LogicType.ELSE:
                break;
        }
    }


}
