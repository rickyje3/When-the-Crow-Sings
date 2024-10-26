using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCondition : DialogueBase
{
   public int conditionIndex = -1;
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
        NOT_EQUAL_TO,
        NO_OPERATOR
    }
    public OperatorType operatorType = OperatorType.EQUAL_TO;

    public string variableKeyString;

    public enum DataType
    {
        UNASSIGNED,
        BOOL,
        INT,
        STRING
    }
    public DataType dataType = DataType.UNASSIGNED;

    public bool boolData;
    public int intData;
    public string stringData;

    public void SetValue(string valueString)
    {

        if (valueString == "true")
        {
            dataType = DataType.BOOL;
            boolData = true;
        }
        else if (valueString == "false")
        {
            dataType = DataType.BOOL;
            boolData = false;
        }
        else if (int.TryParse(valueString, out intData))
        {
            dataType = DataType.INT;
        }
        else
        {
            dataType = DataType.STRING;
            stringData = valueString;
        }
    }

    // A line that acts as a logic gate. Can be if, elif, or else.
    // ...yeah not sure how to do this yet.
}
