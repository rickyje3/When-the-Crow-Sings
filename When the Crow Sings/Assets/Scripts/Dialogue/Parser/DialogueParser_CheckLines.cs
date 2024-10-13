using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public partial class DialogueParser
{
    private void CheckWhichLineType(int i, int myTabCount) // Parse the dialogue line type.
    {
        // if
        if (CheckLine_IsTitle(i, myTabCount)) return;

        // else if, else if, else if, else if, else if...
        if (CheckLine_IsGoto(myTabCount)) return;
        if (CheckLine_IsChoice(i, myTabCount)) return;
        if (CheckLine_IsConditional(myTabCount)) return;
        if (CheckLine_IsMutation(myTabCount)) return;

        // else
        CheckLine_IsNormalLine(myTabCount);
    }

    private bool CheckLine_IsTitle(int i, int myTabCount) // Title
    {
        if (trimmedLine.StartsWith('~'))
        {
            DialogueTitle newLine = new DialogueTitle();
            newLine.tabCount = myTabCount;

            trimmedLine = Regex.Replace(trimmedLine, @"\s+", "");
            trimmedLine = Utilities.RemoveFirstOccurence("~", trimmedLine);

            newLine.titleName = trimmedLine;
            newLine.titleIndex = i;

            dialogueResource.dialogueLines.Add(newLine);
            //dialogueResource.dialogueTitles.Add(newLine); // TODO: make like a getter for this instead.
            //if (dialogueResource.dialogueTitles.Count(x => x.titleName == newLine.titleName) > 1)
            //{
            //    throw new System.Exception("CHELLE SERIOUSLY I WARNED YOU ABOUT THIS! Error: Multiple titles with the same name detected in the same document. >:(");
            //}
            return true;
        }
            return false;
    }

    private bool CheckLine_IsGoto(int myTabCount) // GoTo
    {
        if (trimmedLine.StartsWith("=>"))
        {
            DialogueGoto newLine = new DialogueGoto();
            newLine.tabCount = myTabCount;

            trimmedLine = Regex.Replace(trimmedLine, @"\s+", ""); // Remove white space
            trimmedLine = Utilities.RemoveFirstOccurence("=>", trimmedLine);

            if (trimmedLine == "END")
            {
                newLine.isEnd = true;
            }
            else
            {
                newLine.isEnd = false;
                newLine.gotoTitleName = trimmedLine;
            }

            dialogueResource.dialogueLines.Add(newLine);
            return true;
        }
        return false;
    }

    private bool CheckLine_IsChoice(int i, int myTabCount) // Choice
    {
        if (trimmedLine.StartsWith('-'))
        {
            DialogueChoice newLine = new DialogueChoice();
            newLine.tabCount = myTabCount;
            newLine.choiceText = trimmedLine;
            newLine.choiceIndex = i;


            //DialogueChoiceBlock choiceBlock;
            //if (dialogueResource.dialogueChoiceBlocks.Count > 0)
            //{
            //    bool hasBeenSet = false;
            //    foreach (DialogueChoiceBlock ii in dialogueResource.dialogueChoiceBlocks)
            //    {
            //        // Check indentation
            //        if (ii.choiceTabCount == myTabCount && !ii.dialogueChoices.Contains(newLine))
            //        {
            //            ii.dialogueChoices.Add(newLine);
            //            hasBeenSet = true;
            //        }
            //    }
            //    if (!hasBeenSet)
            //    {
            //        choiceBlock = new DialogueChoiceBlock();
            //        dialogueResource.dialogueChoiceBlocks.Add(choiceBlock);

            //        choiceBlock.choiceTabCount = myTabCount;

            //        choiceBlock.dialogueChoices.Add(newLine);
            //    }

            //}
            //else
            //{
            //    choiceBlock = new DialogueChoiceBlock();
            //    dialogueResource.dialogueChoiceBlocks.Add(choiceBlock);

            //    choiceBlock.choiceTabCount = myTabCount;

            //    choiceBlock.dialogueChoices.Add(newLine);

            //}

            dialogueResource.dialogueLines.Add(newLine);
            
            return true;
        }
        return false;
    }
  

    private bool CheckLine_IsConditional(int myTabCount) // Conditional
    {
        if (
            (trimmedLine.StartsWith("if") || trimmedLine.StartsWith("elif") || trimmedLine.StartsWith("else"))
                    && trimmedLine.EndsWith(":"))
        {
            DialogueCondition newLine = new DialogueCondition();
            newLine.tabCount = myTabCount;
            PrepareConditional(trimmedLine, newLine);

            dialogueResource.dialogueLines.Add(newLine);

            return true;
        }
        return false;
    }

    private bool CheckLine_IsMutation(int myTabCount) // Mutation
    {
        if (trimmedLine.StartsWith("do ") && trimmedLine.EndsWith(';')) 
        {
            DialogueMutation newLine = new DialogueMutation();
            newLine.tabCount = myTabCount;

            dialogueResource.dialogueLines.Add(newLine);

            return true;
        }
        return false;
    }

    private void CheckLine_IsNormalLine(int myTabCount)
    {
        DialogueResponse newLine = new DialogueResponse();
        newLine.tabCount = myTabCount;
        if (trimmedLine.Split(":").Length > 1)
        {
            string[] split = trimmedLine.Split(":", 2);
            newLine.dialogue = string.Join("", split.Skip(1));

            if (split[0].Split('_').Length > 1)
            {
                string[] split2 = split[0].Split('_');
                newLine.characterName = split2[0];
                newLine.characterEmotion = split2[1];
            }
            else
            {
                newLine.characterName = split[0];
                newLine.characterEmotion = "default";
            }
        }
        else
        {
            newLine.characterName = "";
            newLine.dialogue = trimmedLine;
        }
        dialogueResource.dialogueLines.Add(newLine);
    }


  

}
