using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

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
    }
    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<DialogueManager>(this);
        
    }

    private void Start()
    {
        DialogueParser parser = new DialogueParser(dialogueResource);
        //foreach (DialogueBase i in dialogueResource.dialogueLines)
        //{
        //    Debug.Log(i);
        //}
        OnDialogueStart();
    }


    void OnDialogueStart()
    {
        ControlLineBehavior(0);
    }



    void ControlLineBehavior(int index)
    {
        DialogueBase newLine = dialogueResource.dialogueLines[index];

        if (newLine is DialogueResponse)
        {
            DialogueResponse newLine2 = (DialogueResponse)newLine;

            nameText.text = newLine2.characterName;
            StartCoroutine(TypeText(dialogueText, newLine2.dialogue,index));
        }
        else if (newLine is DialogueGoto)
        {
            DialogueGoto newLine2 = (DialogueGoto)newLine;
            if (newLine2.isEnd)
            {
                // SUDO: End dialogue.
            }
            else
            {
                // SUDO find the index and make that the new index yeah
                DialogueTitle tempHolderForTheTargetIndex = dialogueResource.dialogueTitles.Find(x => x.titleName == newLine2.gotoTitleName);
                Debug.Log(newLine2.gotoTitleName + " so we're going to " + tempHolderForTheTargetIndex.titleIndex);
                ControlLineBehavior(tempHolderForTheTargetIndex.titleIndex);

            }
        }
        else
        {
            ControlLineBehavior(index+1);
        }

        
    }


    IEnumerator TypeText(TextMeshProUGUI textMesh, string text, int index)
    {
        dialogueText.maxVisibleCharacters = 0;
        textMesh.text = text;

        while (textMesh.maxVisibleCharacters <= textMesh.text.Length)
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
        ControlLineBehavior(index+1);
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
