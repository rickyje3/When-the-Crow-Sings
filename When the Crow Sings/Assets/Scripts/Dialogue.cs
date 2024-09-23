using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(2, 5)]
    public string[] sentences;
    [TextArea(1, 5)]
    public string[] choices;
    [TextArea(2, 5)]
    public string[] sentencesAfterChoice1;
    [TextArea(2, 5)]
    public string[] sentencesAfterChoice2;
}
