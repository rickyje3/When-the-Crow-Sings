using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public partial class DialogueParser
{
    private DialogueResource dialogueResource;
    string[] raw_lines;
    string rawText;
    string trimmedLine;

    public DialogueParser(DialogueResource targetDialogueResource)
    {
        dialogueResource = targetDialogueResource;

        if (dialogueResource.dialogueLines.Count == 0)
        {
            Prepare(File.ReadAllText(dialogueResource.path));
        }
        
    }


    void Prepare(string text)
    {
        // Identify all DialogueBase line types and add them to dialogueResource.dialogueLines.
        IdentifyAndAssignDialogueLines(text);

        

        // TODO: Figure out how to sort things so "blocks" can be structured.
        DialogueTitle currentTitle = null;
        DialogueTitleBlock currentTitleBlock = null;

        DialogueChoiceBlock currentChoiceBlock = null;

        
        foreach (DialogueBase i in dialogueResource.dialogueLines)
        {
            // Start a new block for every title we encounter.
            if (i is DialogueTitle)
            {
                currentTitle = (DialogueTitle)i;
                currentTitleBlock = new DialogueTitleBlock(currentTitle);
                dialogueResource.dialogueTitleBlocks.Add(currentTitleBlock);

                Debug.Log("We've encountered a title.");
                continue;
            }

            if (dialogueResource.dialogueTitleBlocks.Count == 0) continue;

            if (currentTitleBlock == null) throw new System.Exception("Uh wait this shouldn't be possible hold up.");

            currentTitleBlock.dialogueLines.Add(i);

            if (i is DialogueChoice)
            {
                DialogueChoice _i = (DialogueChoice)i;

                if (currentChoiceBlock != null)
                {
                    bool hasBeenSet = false;
                    foreach (DialogueChoiceBlock ii in currentTitleBlock.dialogueChoiceBlocks)
                    {
                        // Check indentation
                        if (ii.choiceTabCount == _i.tabCount && !ii.dialogueChoices.Contains(_i))
                        {
                            ii.dialogueChoices.Add(_i);
                            hasBeenSet = true;
                        }
                    }

                    if (!hasBeenSet)
                    {
                        currentChoiceBlock = new DialogueChoiceBlock();
                        currentTitleBlock.dialogueChoiceBlocks.Add(currentChoiceBlock);

                        currentChoiceBlock.choiceTabCount = _i.tabCount;

                        currentChoiceBlock.dialogueChoices.Add(_i);
                    }
                }
                else
                {
                    currentChoiceBlock = new DialogueChoiceBlock();
                    currentTitleBlock.dialogueChoiceBlocks.Add(currentChoiceBlock);

                    currentChoiceBlock.choiceTabCount = _i.tabCount;

                    currentChoiceBlock.dialogueChoices.Add(_i);
                }
            }
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
        }
    }

    

}
