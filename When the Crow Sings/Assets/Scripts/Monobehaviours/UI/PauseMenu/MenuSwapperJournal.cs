using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwapperJournal : MenuSwapper
{
    public GameObject journalHolder;

    protected override void AdditionalMenuLogic(int whichMenu)
    {
        // TODO: try to figure out the easiest way to put this in a child class maybe so it's not clogging up everything else...
        if (whichMenu > 1) journalHolder.SetActive(true);
        else journalHolder.SetActive(false);
    }
}
