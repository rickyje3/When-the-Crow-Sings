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
    [SerializeField] private GameObject dialogueChoiceButtonsHolder;
    [SerializeField] private List<GameObject> dialogueChoiceButtons;

    [Header("Signals")]
    public GameSignal startDialogueSignal;
    public GameSignal finishDialogueSignal;

    [Header("Settings")]
    public float textSpeed = .05f;
    public float pauseMultiplier = 10f;
    public List<GameSignal> signalsDialogueCanUse;

    DialogueChoiceBlock activeChoiceBlock = null;
    DialogueConditionBlock activeConditionBlock = null;

    #region StartMethods()
    private void Awake()
    {
        RegisterSelfAsService();
        dialogueUI.SetActive(false);
        
    }
    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<DialogueManager>(this);
    }
    #endregion









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
        dialogueChoiceButtonsHolder.SetActive(false);

        DialogueParser parser = new DialogueParser(dialogueResource);
        //DialogueTitle tempHolderForTheTargetIndex = dialogueResource.dialogueTitles.Find(x => x.titleName == signalArgs.stringArgs[0]); // TODO: Error if no title is found. Though maybe the built-in ones are clear enough.
        DialogueTitle tempHolderForTheTargetIndex = dialogueResource.dialogueLines.OfType<DialogueTitle>().ToList().Find(x => x.titleName == signalArgs.stringArgs[0]); // TODO: Error if no title is found. Though maybe the built-in ones are clear enough.

        //dialogueResource.dialogueLines.OfType<DialogueTitle>().ToList().Count(x => x.titleName == newLine.titleName)

        ControlLineBehavior(tempHolderForTheTargetIndex.titleIndex,tempHolderForTheTargetIndex.tabCount);

    }

    public void EndDialogue()
    {
        //InputManager.playerInputActions.Player.Enable();
        dialogueUI.SetActive(false);
        finishDialogueSignal.Emit();
    }



    void ControlLineBehavior(int index, int previousLineTabCount)
    {
        canNextLine = false;
        currentLine = index;
        DialogueBase newLine = dialogueResource.dialogueLines[index];
        
        // Check if we need to skip to after a choice block.
        if (activeChoiceBlock != null && activeChoiceBlock.choiceHasBeenMade && newLine.tabCount <= activeChoiceBlock.choiceTabCount)
        {
            activeChoiceBlock.choiceHasBeenMade = false;
            ControlLineBehavior(activeChoiceBlock.endIndex, newLine.tabCount);
            return;
        }

        // Check if we need to skip to after a condition block.
        if (activeConditionBlock != null && activeConditionBlock.conditionHasBeenDecided && newLine.tabCount <= activeConditionBlock.conditionTabCount)
        {
            activeConditionBlock.conditionHasBeenDecided = false;
            //Debug.Log("Should be "+ ((DialogueResponse)dialogueResource.dialogueLines[activeConditionBlock.endIndex]).dialogue);
            ControlLineBehavior(activeConditionBlock.endIndex, newLine.tabCount);
            return;
        }


        if (newLine is DialogueResponse)
        {
            
            DialogueResponse newLine2 = (DialogueResponse)newLine;
            Debug.Log(newLine2.dialogue);
            nameText.text = newLine2.characterName;
            StartCoroutine(TypeText(dialogueText, newLine2.dialogue,index));
        }
       
        else if (newLine is DialogueGoto)
        {
            ResetChoiceBlocks();
            ResetConditionBlocks();

            DialogueGoto newLine2 = (DialogueGoto)newLine;
            if (newLine2.isEnd)
            {
                EndDialogue();
            }
            else
            {
                DialogueTitle tempHolderForTheTargetIndex = dialogueResource.dialogueLines.OfType<DialogueTitle>().ToList().Find(x => x.titleName == newLine2.gotoTitleName);
                Debug.Log(newLine2.gotoTitleName + " so we're going to " + tempHolderForTheTargetIndex.titleIndex);
                ControlLineBehavior(tempHolderForTheTargetIndex.titleIndex, previousLineTabCount);
            }
        }

        else if (newLine is DialogueChoice)
        {
            dialogueChoiceButtonsHolder.SetActive(true);

            //activeChoiceBlock = null;
            foreach (DialogueTitleBlock i in dialogueResource.dialogueTitleBlocks)
            {
                foreach (DialogueChoiceBlock ii in i.dialogueChoiceBlocks)
                {
                    if (ii.dialogueChoices.Contains(newLine))
                    {
                        activeChoiceBlock = ii;
                        //Debug.Log("alasdhflaskgdjhklasdfh");
                        break;
                    }
                }
                //Debug.Log("Well, nothing in that title block.");
            }


            if (activeChoiceBlock == null) { throw new Exception("THE THING IS BLANK YOU SILLY GOOSE"); }

            SetChoiceButtons();

        }

        else if (newLine is DialogueCondition)
        {
            foreach (DialogueTitleBlock i in dialogueResource.dialogueTitleBlocks)
            {
                foreach (DialogueConditionBlock ii in i.dialogueConditionBlocks)
                {
                    if (ii.allConditions.Contains(newLine))
                    {
                        activeConditionBlock = ii;
                        break;
                    }
                }
            }
            
            if (activeConditionBlock == null) { throw new Exception("THE CONDITION BLOCK IS BLANK YOU SILLY DUCK"); }

            Debug.Log("About to call DoConditionalDialogueLogic()");
            DoConditionalDialogueLogic();


        }

        else // In case of an EmptyLine
        {
            ControlLineBehavior(index+1,previousLineTabCount);
        }
    }

    private void SetChoiceButtons()
    {
        foreach (GameObject i in dialogueChoiceButtons)
        {
            i.SetActive(false);
        }
        int loop = 0;
        foreach (DialogueChoice i in activeChoiceBlock.dialogueChoices)
        {
            dialogueChoiceButtons[loop].SetActive(true);
            dialogueChoiceButtons[loop].GetComponentInChildren<TextMeshProUGUI>().text = i.choiceText;
            dialogueChoiceButtons[loop].GetComponent<DialogueChoiceButton>().dialogueLineIndex = i.choiceIndex;
            dialogueChoiceButtons[loop].GetComponent<DialogueChoiceButton>().dialogueChoice = i;
            loop++;
        }
    }

    private void ResetChoiceBlocks()
    {
        foreach (DialogueTitleBlock i in dialogueResource.dialogueTitleBlocks)
        {
            foreach (DialogueChoiceBlock ii in i.dialogueChoiceBlocks)
            {
                ii.choiceHasBeenMade = false;
            }
        }
    }
    private void ResetConditionBlocks()
    {
        foreach (DialogueTitleBlock i in dialogueResource.dialogueTitleBlocks)
        {
            foreach (DialogueConditionBlock ii in i.dialogueConditionBlocks)
            {
                ii.conditionHasBeenDecided = false;
            }
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
            //DialogueTitle tempHolderForTheTargetIndex = dialogueResource.dialogueTitles.Find(x => x.titleName == newLine2.gotoTitleName);
            
            ControlLineBehavior(currentLine + 1, dialogueResource.dialogueLines[currentLine].tabCount);
        }
        
    }

    public void OnDialogueChoiceButtonClicked(DialogueChoiceButton choiceButton)
    {
        dialogueChoiceButtonsHolder.SetActive(false);

        //activeChoiceBlock.Reset();
        activeChoiceBlock.choiceHasBeenMade = true;

        int nextLine = choiceButton.dialogueLineIndex + 1;
        int choiceTabCount = choiceButton.dialogueChoice.tabCount;

        
        ControlLineBehavior(nextLine, choiceTabCount);
    }

    void DoConditionalDialogueLogic()
    {
        foreach (DialogueCondition i in activeConditionBlock.allConditions)
        {
            Debug.Log("Condition is " + i.variableKeyString + " and datatype is " + i.dataType);
        }

        //switch (dialogueCondition.logicType)
        //{
        //    case DialogueCondition.LogicType.IF:
        //        switch (dialogueCondition.operatorType)
        //        {
        //            case DialogueCondition.OperatorType.EQUAL_TO:

        //                break;

        //            case DialogueCondition.OperatorType.GREATER_THAN:

        //                break;

        //            case DialogueCondition.OperatorType.GREATER_THAN_OR_EQUAL_TO:

        //                break;

        //            case DialogueCondition.OperatorType.LESS_THAN:

        //                break;

        //            case DialogueCondition.OperatorType.LESS_THAN_OR_EQUAL_TO:

        //                break;

        //            case DialogueCondition.OperatorType.NOT_EQUAL_TO:

        //                break;
        //        }
        //        break;

        //    case DialogueCondition.LogicType.ELIF:
        //        break;
        //    case DialogueCondition.LogicType.ELSE:
        //        break;
        //}














        activeConditionBlock.conditionHasBeenDecided = true;
        Debug.Log("Target index is "+(activeConditionBlock.ifStatement.conditionIndex + 1));
        ControlLineBehavior(activeConditionBlock.ifStatement.conditionIndex+1, activeConditionBlock.ifStatement.tabCount);



        
    }


}
