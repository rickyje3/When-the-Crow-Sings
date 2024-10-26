using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoiceBlock : DialogueBlock
{
    public List<DialogueChoice> dialogueChoices = new List<DialogueChoice>();
    public int choiceTabCount = -1;


    // CURRENTLY UNUSED!!!
    // Where the block starts.
    public int startIndex = -1;
    // Where the block terminates. Determined by if there is a line of lower indentation.
    public int endIndex = -1;
}
