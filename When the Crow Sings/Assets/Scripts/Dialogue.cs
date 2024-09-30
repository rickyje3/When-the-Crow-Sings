using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(2, 5)]
    public string[] sentences;
    [TextArea(1, 5)]
    public string[] choices1;
    [TextArea(1, 5)]
    public string[] choices2;
    [TextArea(2, 5)]
    public string[] sentencesAfterChoice1;
    [TextArea(2, 5)]
    public string[] sentencesAfterChoice2;
}
