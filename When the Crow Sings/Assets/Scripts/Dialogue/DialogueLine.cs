using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueLine
{
    public enum LINE_TYPE
    {
        UNKNOWN,
        RESPONSE,
        TITLE,
        CONDITION,
        MUTATION,
        GOTO,
        DIALOGUE,
        ERROR,

        EMPTY,
        
        ELSE
    }

    public LINE_TYPE type = LINE_TYPE.UNKNOWN;

    public string fullDialogueLine = "";
    public string dialogue = "";

    public int nextID;

    public string characterName;
    public string characterEmotion; // enum?

    public Dictionary<int,float> pauses; // TODO: figure out type. int index and float length?

    public DialogueLine(string rawLine)
    {
        fullDialogueLine = rawLine.Trim();

        if (fullDialogueLine.Split(":").Length > 1)
        {
            type = LINE_TYPE.DIALOGUE;
            string[] split = fullDialogueLine.Split(":", 2);
            characterName = split[0];
            dialogue = string.Join("", split.Skip(1));


            Debug.Log("Name: "+characterName);
            Debug.Log("Dialogue: "+dialogue);
        }

        switch (fullDialogueLine)
        {
            case "Yeah":
                
                break;
            default:
                break;
        };


    }
}
