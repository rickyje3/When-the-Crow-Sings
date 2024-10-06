using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCondition : DialogueBase
{
    public enum LogicType
    {
        IF,
        ELIF,
        ELSE
    }
    public LogicType logicType = LogicType.IF;

    public enum OperatorType
    {
        EQUAL_TO,
        GREATER_THAN,
        LESS_THAN,
        GREATER_THAN_OR_EQUAL_TO,
        LESS_THAN_OR_EQUAL_TO,
        NOT_EQUAL_TO
    }
    public OperatorType operatorType = OperatorType.EQUAL_TO;

    // A line that acts as a logic gate. Can be if, elif, or else.
    // ...yeah not sure how to do this yet.
}
