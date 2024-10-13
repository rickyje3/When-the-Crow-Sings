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

        // TODO: Figure out how to sort things so "blocks" can be structured. Either: Do it all at once, or iterate through everything a second time.
    }

    private void IdentifyAndAssignDialogueLines(string text)
    {
        raw_lines = text.Split("\n");

        for (int i = 0; i < raw_lines.Length; i++)
        {
            int myTabCount = -1;
            trimmedLine = raw_lines[i];

            CountTabs(i, ref myTabCount);

            // Skip empty lines.
            if (string.IsNullOrEmpty(trimmedLine))
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

}
