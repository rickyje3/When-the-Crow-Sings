using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueConditionBlock : DialogueBlock
{
    public DialogueCondition ifStatement = null;
    public List<DialogueCondition> elifStatements = null;
    public DialogueCondition elseStatement = null;

    public int conditionTabCount = -1;
    public bool conditionHasBeenDecided = false;

    public int endIndex = -1;
}
