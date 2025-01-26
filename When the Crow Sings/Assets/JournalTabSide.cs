using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalTabSide : JournalTab
{
    public override void SetActivateTab(bool activated)
    {

    }

    public override void ActivateTab()
    {
        Debug.Log("Ping!");
    }

    public override void DeactivateTab()
    {
        Debug.Log("!gniP");
    }    
}
