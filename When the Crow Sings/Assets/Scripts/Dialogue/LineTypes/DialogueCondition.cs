using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCondition : DialogueBase
{
    enum LogicType
    {
        IF,
        ELIF,
        ELSE
    }
    LogicType logicType = LogicType.IF;

    // A line that acts as a logic gate. Can be if, elif, or else.
    // ...yeah not sure how to do this yet.
}
