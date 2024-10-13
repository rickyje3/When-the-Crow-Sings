using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTitleBlock
{
    public DialogueTitle dialogueTitle;

    // Blocks of related lines.
    public List<DialogueConditionBlock> dialogueConditionBlocks = new List<DialogueConditionBlock>();
    public List<DialogueChoiceBlock> dialogueChoiceBlocks = new List<DialogueChoiceBlock>();
}
