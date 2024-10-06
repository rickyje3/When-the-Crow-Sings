using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueResponse : DialogueBase
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

    public string characterName;
    public string characterEmotion; // enum?

    public Dictionary<int,float> pauses; // TODO: figure out type. int index and float length?
}
