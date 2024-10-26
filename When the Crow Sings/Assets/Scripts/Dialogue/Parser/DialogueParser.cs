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
            PrepareDialogueResource(File.ReadAllText(dialogueResource.path));
        }
        
    }

    // TODO: Figure out how to sort things so "blocks" can be structured.
    DialogueTitle currentTitle = null;
    DialogueTitleBlock currentTitleBlock = null;

    DialogueChoiceBlock currentChoiceBlock = null;


    void PrepareDialogueResource(string text) // 
    {
        // Identify all DialogueBase line types and add them to dialogueResource.dialogueLines.
        IdentifyAndAssignDialogueLines(text);

        foreach (DialogueBase i in dialogueResource.dialogueLines)
        {
           

            // Start a new block for every title we encounter.
            if (i is DialogueTitle)
            {
                currentTitle = (DialogueTitle)i;
                currentTitleBlock = new DialogueTitleBlock(currentTitle);
                dialogueResource.dialogueTitleBlocks.Add(currentTitleBlock);

                //Debug.Log("We've encountered a title.");
                continue;
            }
            if (dialogueResource.dialogueTitleBlocks.Count == 0) continue;
            

            // Since this line is not a title, the current line to the TitleBlock.
            if (currentTitleBlock == null) throw new System.Exception("Uh wait this shouldn't be possible hold up.");
            currentTitleBlock.dialogueLines.Add(i);

            // if currentchoiceblock != null and if this line is a lower indentation than the block, then create a new choice block.
            // but also, if the next line is the same indentation and NOT a choice block, then create a new choice block.

            if (currentChoiceBlock != null && i.tabCount <= currentChoiceBlock.choiceTabCount && i is not DialogueChoice)
            {
                currentChoiceBlock.endIndex = 1;
            }

            if (currentChoiceBlock == null)
            {
                if (i is DialogueChoice)
                {
                    DialogueChoice _i = (DialogueChoice)i;

                    currentChoiceBlock = new DialogueChoiceBlock();
                    currentTitleBlock.dialogueChoiceBlocks.Add(currentChoiceBlock);

                    currentChoiceBlock.choiceTabCount = _i.tabCount;

                    currentChoiceBlock.dialogueChoices.Add(_i);
                }
            }
            else
            {
                if (i is DialogueChoice)
                {
                    DialogueChoice _i = (DialogueChoice)i;



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

                }
            }
        }
    }
}
