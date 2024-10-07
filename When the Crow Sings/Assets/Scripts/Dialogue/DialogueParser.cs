using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class DialogueParser
{
    public DialogueParser(DialogueResource dialogueResource) // Or the actual Asset, not a string filePath.
    {
        Prepare(dialogueResource,File.ReadAllText(dialogueResource.path));
    }



    string[] raw_lines;
    public List<DialogueBase> allLines = new List<DialogueBase>();
    void Prepare(DialogueResource dialogueResource, string text)
    {
        raw_lines = text.Split("\n");

        // ???var non_empty_lines = raw_lines - raw_lines.emptylines()
        //

        for (int i = 0;  i < raw_lines.Length; i++)
        {
            string trimmedLine = raw_lines[i].Trim();


            // Skip empty lines.
            if (string.IsNullOrEmpty(trimmedLine) )
            {
                allLines.Add(new DialogueEmpty());
                continue;
            }

            // Count the number of indents/tabs.
            bool hasFinishedCountingTabs = false;
            int myTabCount = 0;
            while (!hasFinishedCountingTabs)
            {

                if (trimmedLine.StartsWith('\t'))
                {
                    myTabCount++;
                    trimmedLine.Remove(0,1); // Remove the tab before checking for any more.
                }
                else
                {
                    hasFinishedCountingTabs = true;
                }
            }

            // Parse the dialogue line type.
            if (trimmedLine.StartsWith('~')) // Title
            {
                //type = LINE_TYPE.TITLE;
                DialogueTitle newLine = new DialogueTitle();
                newLine.tabCount = myTabCount;

                trimmedLine = Regex.Replace(trimmedLine, @"\s+", "");
                trimmedLine = Utilities.RemoveFirstOccurence("~", trimmedLine);

                newLine.titleName = trimmedLine;
                newLine.titleIndex = i;

                allLines.Add(newLine);
                dialogueResource.dialogueTitles.Add(newLine); // TODO: make like a getter for this instead.
                if (dialogueResource.dialogueTitles.Count(x => x.titleName == newLine.titleName) > 1)
                {
                    throw new System.Exception("CHELLE SERIOUSLY I WARNED YOU ABOUT THIS! Error: Multiple titles with the same name detected in the same document.");
                }
            }

            else if (trimmedLine.StartsWith("=>")) // GoTo
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
                    Debug.Log("Title is = " + trimmedLine);
                }

                allLines.Add(newLine);
            }

            else if (trimmedLine.StartsWith('-')) // Choice
            {
                DialogueChoice newLine = new DialogueChoice();
                newLine.tabCount = myTabCount;

                allLines.Add(newLine);
            }

            else if ((trimmedLine.StartsWith("if") || trimmedLine.StartsWith("elif") || trimmedLine.StartsWith("else"))
                && trimmedLine.EndsWith(":")) // Conditional
            {
                DialogueCondition newLine = new DialogueCondition();
                newLine.tabCount = myTabCount;
                PrepareConditional(trimmedLine, newLine);

                allLines.Add(newLine);
            }

            else if (trimmedLine.StartsWith("do ") && trimmedLine.EndsWith(';')) // Mutation
            {
                DialogueMutation newLine = new DialogueMutation();
                newLine.tabCount = myTabCount;

                allLines.Add(newLine);
            }

            else // Normal Dialogue Line
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
                allLines.Add(newLine);
            }

            
        }
        // After finding all the lines, set the dialogueResource's lines.
        dialogueResource.dialogueLines = allLines;
    }

    void PrepareConditional(string trimmedLine, DialogueCondition newLine)
    {
        if (trimmedLine.StartsWith("if"))
        {
            newLine.logicType = DialogueCondition.LogicType.IF;
            trimmedLine = Utilities.RemoveFirstOccurence("if ", trimmedLine);
            trimmedLine = Utilities.RemoveFirstOccurence(":", trimmedLine);
            // TODO Use "if" as a way to determine "blocks of conditionals" to group together.
            // Presumably all else-ifs and elses of the same tabCount or something like that.
        }
        else if (trimmedLine.StartsWith("elif"))
        {
            newLine.logicType = DialogueCondition.LogicType.ELIF;
            trimmedLine = Utilities.RemoveFirstOccurence("elif ", trimmedLine);
            trimmedLine = Utilities.RemoveFirstOccurence(":", trimmedLine);
        }
        else
        {
            newLine.logicType = DialogueCondition.LogicType.ELSE;
            trimmedLine = Utilities.RemoveFirstOccurence("else:", trimmedLine);
        }

        string[] variableKeys = null;
        trimmedLine = Regex.Replace(trimmedLine, @"\s+", ""); // We're removing all whitespace.

        if (newLine.logicType != DialogueCondition.LogicType.ELSE)
        {
            if (trimmedLine.Contains("=="))
            {
                newLine.operatorType = DialogueCondition.OperatorType.EQUAL_TO;
                variableKeys = trimmedLine.Split(new string[] { "==" }, System.StringSplitOptions.None);
            }
            else if (trimmedLine.Contains(">="))
            {
                newLine.operatorType = DialogueCondition.OperatorType.GREATER_THAN_OR_EQUAL_TO;
                variableKeys = trimmedLine.Split(new string[] { ">=" }, System.StringSplitOptions.None);
            }
            else if (trimmedLine.Contains(">"))
            {
                newLine.operatorType = DialogueCondition.OperatorType.GREATER_THAN;
                variableKeys = trimmedLine.Split(new string[] { ">" }, System.StringSplitOptions.None);
            }
            else if (trimmedLine.Contains("<="))
            {
                newLine.operatorType = DialogueCondition.OperatorType.LESS_THAN_OR_EQUAL_TO;
                variableKeys = trimmedLine.Split(new string[] { "<=" }, System.StringSplitOptions.None);
            }
            else if (trimmedLine.Contains("<"))
            {
                newLine.operatorType = DialogueCondition.OperatorType.LESS_THAN;
                variableKeys = trimmedLine.Split(new string[] { "<" }, System.StringSplitOptions.None);
            }
            else if (trimmedLine.Contains("!="))
            {
                newLine.operatorType = DialogueCondition.OperatorType.NOT_EQUAL_TO;
                variableKeys = trimmedLine.Split(new string[] { "!=" }, System.StringSplitOptions.None);
            }

            // Set the terms on each side of the operator (as strings).
            newLine.variableKey1 = variableKeys[0];
            newLine.variableKey2 = variableKeys[1];
        }
        else
        {
            newLine.operatorType = DialogueCondition.OperatorType.NO_OPERATOR;
            // Presumably the DialogueManager will handle variableKey stuff since there aren't any.
        }
        
    }

    string rawText;
    void Parse(string text)
    {

    }






    //string GetNextLine()
    //{
    //    currentLine += 1;
    //    StreamReader reader = new StreamReader(_filePath);
    //    //reader.


    //    return "";
    //}
}
