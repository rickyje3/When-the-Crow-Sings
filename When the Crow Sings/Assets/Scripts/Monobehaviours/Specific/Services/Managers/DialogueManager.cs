using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class DialogueManager : MonoBehaviour, IService
{

    
    private DialogueResource dialogueResource;
    

    [Header("Dialogue UI Elements")]
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject dialogueChoicesHolder;
    [SerializeField] private List<GameObject> dialogueChoiceButtons;


    [Header("Signals")]
    public GameSignal startDialogueSignal;
    public GameSignal finishDialogueSignal;

    [Header("Settings")]
    public float textSpeed = .05f;
    public float pauseMultiplier = 10f;
    public List<GameSignal> signalsDialogueCanUse;


    private void Awake()
    {
        RegisterSelfAsService();
        dialogueUI.SetActive(false);
    }
    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<DialogueManager>(this);
    }










    public void StartDialogue(SignalArguments signalArgs)
    {
        if (signalArgs.objectArgs[0] is DialogueResource)
        {
            dialogueResource = (DialogueResource)signalArgs.objectArgs[0];
        }
        else
        {
            throw new Exception("Error! The component emitting the signal does not have a DialogueResource as its first ObjectArgument.");
        }

        //InputManager.playerInputActions.Player.Disable();
        dialogueUI.SetActive(true);

        DialogueParser parser = new DialogueParser(dialogueResource);
        DialogueTitle tempHolderForTheTargetIndex = dialogueResource.dialogueTitles.Find(x => x.titleName == signalArgs.stringArgs[0]); // TODO: Error if no title is found. Though maybe the built-in ones are clear enough.

        ControlLineBehavior(tempHolderForTheTargetIndex.titleIndex);

    }

    public void EndDialogue()
    {
        //InputManager.playerInputActions.Player.Enable();
        dialogueUI.SetActive(false);
        finishDialogueSignal.Emit();
    }



    void ControlLineBehavior(int index)
    {
        canNextLine = false;
        currentLine = index;
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
                EndDialogue();
            }
            else
            {
                DialogueTitle tempHolderForTheTargetIndex = dialogueResource.dialogueTitles.Find(x => x.titleName == newLine2.gotoTitleName);
                Debug.Log(newLine2.gotoTitleName + " so we're going to " + tempHolderForTheTargetIndex.titleIndex);
                ControlLineBehavior(tempHolderForTheTargetIndex.titleIndex);
            }
        }

        else if (newLine is DialogueChoice)
        {
            DialogueChoiceBlock choiceBlock = null;
            foreach (DialogueChoiceBlock i in dialogueResource.dialogueChoiceBlocks)
            {
                if (i.dialogueChoices.Contains(newLine))
                {
                    choiceBlock = i;
                    break;
                }
            }
            if (choiceBlock == null) { throw new Exception("THE THING IS BLANK YOU SILLY GOOSE"); }

            foreach (DialogueChoice i in choiceBlock.dialogueChoices)
            {
                Debug.Log("The choice is "+i.choiceText + " and its tab count is "+i.tabCount);
            }

            // TODO: Populate the buttons. Then, wait for an inputevent from one of them to call ControlLineBehavior() again.

        }

        else // In case of an EmptyLine
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
        //ControlLineBehavior(index+1);
        canNextLine = true;
    }

    private int currentLine;
    private bool canNextLine = false;
    public void NextLine()
    {
        // SUDO: if goto, empty, or conditional, get/"do"(?) the next-next line automatically. Otherwise, wait for input/signal/whatever.
        // only exception is Choices, which need to be "got" on "finished typing" not "on input").

        if (canNextLine)
        {
            ControlLineBehavior(currentLine + 1);
        }
        
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
