using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueConditionBlock : DialogueBlock
{
    public DialogueCondition ifStatement = null;
    public DialogueCondition[] elifStatements = null;
    public DialogueCondition elseStatement = null;
}
