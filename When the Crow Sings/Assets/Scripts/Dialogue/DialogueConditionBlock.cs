using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueConditionBlock : DialogueBlock
{
    public DialogueCondition ifStatement = null;
    public List<DialogueCondition> elifStatements = new List<DialogueCondition>();
    public DialogueCondition elseStatement = null;

    public List<DialogueCondition> allConditions = new List<DialogueCondition>();

    public int conditionTabCount = -1;
    public bool conditionHasBeenDecided = false;

    public int endIndex = -1;

    public void AddCondition(DialogueCondition condition)
    {
        if (condition.logicType == DialogueCondition.LogicType.IF)
        {
            ifStatement = condition;
        }
        else if (condition.logicType == DialogueCondition.LogicType.ELIF)
        {
            elifStatements.Add(condition);
        }
        else
        {
            elseStatement = condition;
        }

        allConditions.Add(condition);
    }
}
