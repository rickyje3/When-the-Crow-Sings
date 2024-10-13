using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGoto : DialogueBase
{
    public bool isEnd = false;
    public string gotoTitleName = "";
    //public int gotoTitleIndex = 0;

    // TODO: if text is "=> END" then "finish the dialogue."
    // TOD: The "target line" should probably be like "that title name.index + 1" or something.
}
