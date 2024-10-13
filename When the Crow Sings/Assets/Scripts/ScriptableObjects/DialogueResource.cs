using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueResource : ScriptableObject
{
    public string path = "Assets/Dialogue/TestText.txt";
    [HideInInspector]
    public List<DialogueBase> dialogueLines = new List<DialogueBase>();
    
    
    // Blocks of related lines.
    [HideInInspector]
    public List<DialogueConditionBlock> dialogueConditionBlocks = new List<DialogueConditionBlock>();
    [HideInInspector]
    public List<DialogueChoiceBlock> dialogueChoiceBlocks = new List<DialogueChoiceBlock>();

    [HideInInspector]
    public List<DialogueTitle> dialogueTitles = new List<DialogueTitle>();
}
