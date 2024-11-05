using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMutation : DialogueBase
{
    // A line that sets a variable, calls a method, or otherwise modifies the game state.

    //public List<string> action_types = new List<string>() { "set","emit","call" }; // pretend it's constant C# doesn't like it with lists.
    
    public enum ActionType { SET,EMIT,CALL,CHELLE_YOU_STINKY }
    public ActionType actionType = ActionType.CHELLE_YOU_STINKY;

    public string actionKey = "";
    
    public int intData;
    public string stringData;
    public bool boolData;

    public enum OperatorType
    {
        EQUALS,
        PLUS_EQUALS,
        MINUS_EQUALS,
        NO_OPERATOR
    }
    public OperatorType operatorType = OperatorType.EQUALS;

    public enum DataType
    {
        UNASSIGNED,
        BOOL,
        INT,
        STRING
    }
    public DataType dataType = DataType.UNASSIGNED;
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


}
