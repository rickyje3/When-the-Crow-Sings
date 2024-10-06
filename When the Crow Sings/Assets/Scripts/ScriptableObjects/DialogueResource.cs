using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueResource : ScriptableObject
{
    public string path = "Assets/Dialogue/TestText.txt";
    [HideInInspector]
    public List<DialogueBase> dialogueLines = new List<DialogueBase>();
    [HideInInspector]
    public List<DialogueConditionBlock> dialogueConditionBlocks = new List<DialogueConditionBlock>();
}
