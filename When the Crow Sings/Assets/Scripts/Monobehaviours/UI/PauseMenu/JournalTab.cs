using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JournalTab : MonoBehaviour
{
    public abstract void SetActivateTab(bool activated);
    public abstract void ActivateTab();
    public abstract void DeactivateTab();
}
