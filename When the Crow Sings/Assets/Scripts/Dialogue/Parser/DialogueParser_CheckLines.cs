using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public partial class DialogueParser
{
    private void IdentifyAndAssignDialogueLines(string text)
    {
        raw_lines = text.Split("\n");

        for (int i = 0; i < raw_lines.Length; i++)
        {
            int myTabCount = -1;
            trimmedLine = raw_lines[i];

            CountTabs(i, ref myTabCount);

            // Skip empty lines.
            if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("//"))
            {
                dialogueResource.dialogueLines.Add(new DialogueEmpty());
                continue;
            }

            CheckWhichLineType(i, myTabCount);
        }
    }
    private void CountTabs(int i, ref int myTabCount) // The ref int is intentional here.
    {
        // Count the number of indents/tabs.
        bool hasFinishedCountingTabs = false;
        myTabCount = 0;
        while (!hasFinishedCountingTabs)
        {
            if (trimmedLine.StartsWith('\t'))
            {
                myTabCount = myTabCount + 1;
                trimmedLine = trimmedLine.Remove(0, 1); // Remove the tab before checking for any more.
            }
            else
            {
                hasFinishedCountingTabs = true;
            }
        }

        // Actually trim the line (couldn't do it earlier because of the tab stuff)
        trimmedLine = trimmedLine.Trim();
    }

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

            if (dialogueResource.dialogueLines.OfType<DialogueTitle>().ToList().Count(x => x.titleName == newLine.titleName) > 1)
            {
                throw new System.Exception("CHELLE SERIOUSLY I WARNED YOU ABOUT THIS! Error: Multiple titles with the same name detected in the same document. >:(");
            }
            
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

            dialogueResource.dialogueLines.Add(newLine);
            
            return true;
        }
        return false;
    }
  

    private bool CheckLine_IsConditional(int myTabCount) // Conditional
    {
        if (
            (trimmedLine.StartsWith("if ") || trimmedLine.StartsWith("elif ") || trimmedLine.StartsWith("else"))
                    && trimmedLine.EndsWith(":"))
        {
            DialogueCondition newLine = new DialogueCondition();
            newLine.tabCount = myTabCount;

            PrepareConditional(trimmedLine, ref newLine);

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
            newLine.dialogue = newLine.dialogue.Trim();

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
