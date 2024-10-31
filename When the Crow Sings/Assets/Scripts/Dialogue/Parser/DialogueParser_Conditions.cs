using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public partial class DialogueParser
{

    void PrepareConditional(string trimmedLine, ref DialogueCondition newLine)
    {
        trimmedLine = AssignConditionalType(ref trimmedLine, ref newLine);

        trimmedLine = Regex.Replace(trimmedLine, @"\s+", ""); // Remove all whitespace.


        string[] variableKeys = null; // Holder for conditional logic.

        if (newLine.logicType != DialogueCondition.LogicType.ELSE)
        {
            if (trimmedLine.Contains("=="))
            {
                newLine.operatorType = DialogueCondition.OperatorType.EQUAL_TO;
                variableKeys = trimmedLine.Split(new string[] { "==" }, System.StringSplitOptions.None);
            }
            else if (trimmedLine.Contains(">="))
            {
                newLine.operatorType = DialogueCondition.OperatorType.GREATER_THAN_OR_EQUAL_TO;
                variableKeys = trimmedLine.Split(new string[] { ">=" }, System.StringSplitOptions.None);
            }
            else if (trimmedLine.Contains(">"))
            {
                newLine.operatorType = DialogueCondition.OperatorType.GREATER_THAN;
                variableKeys = trimmedLine.Split(new string[] { ">" }, System.StringSplitOptions.None);
            }
            else if (trimmedLine.Contains("<="))
            {
                newLine.operatorType = DialogueCondition.OperatorType.LESS_THAN_OR_EQUAL_TO;
                variableKeys = trimmedLine.Split(new string[] { "<=" }, System.StringSplitOptions.None);
            }
            else if (trimmedLine.Contains("<"))
            {
                newLine.operatorType = DialogueCondition.OperatorType.LESS_THAN;
                variableKeys = trimmedLine.Split(new string[] { "<" }, System.StringSplitOptions.None);
            }
            else if (trimmedLine.Contains("!="))
            {
                newLine.operatorType = DialogueCondition.OperatorType.NOT_EQUAL_TO;
                variableKeys = trimmedLine.Split(new string[] { "!=" }, System.StringSplitOptions.None);
            }

            // Set the terms on each side of the operator (as strings).
            newLine.variableKeyString = variableKeys[0];
            //newLine.variableValueString = variableKeys[1];
            newLine.SetValue(variableKeys[1]);
        }
        else
        {
            newLine.operatorType = DialogueCondition.OperatorType.NO_OPERATOR;
            // Presumably the DialogueManager will handle variableKey stuff since there aren't any.
        }

    }

    private static string AssignConditionalType(ref string trimmedLine, ref DialogueCondition newLine)
    {
        if (trimmedLine.StartsWith("if "))
        {
            newLine.logicType = DialogueCondition.LogicType.IF;
            trimmedLine = Utilities.RemoveFirstOccurence("if ", trimmedLine);
            trimmedLine = Utilities.RemoveFirstOccurence(":", trimmedLine);
            // TODO Use "if" as a way to determine "blocks of conditionals" to group together.
            // Presumably all else-ifs and elses of the same tabCount or something like that.
        }
        else if (trimmedLine.StartsWith("elif "))
        {
            newLine.logicType = DialogueCondition.LogicType.ELIF;
            trimmedLine = Utilities.RemoveFirstOccurence("elif ", trimmedLine);
            trimmedLine = Utilities.RemoveFirstOccurence(":", trimmedLine);
        }
        else
        {
            newLine.logicType = DialogueCondition.LogicType.ELSE;
            trimmedLine = Utilities.RemoveFirstOccurence("else:", trimmedLine);
        }

        return trimmedLine;
    }
}
