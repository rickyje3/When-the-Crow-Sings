using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalTabSide : JournalTab
{
    public bool isActivated = false;
    public override void SetActivateTab(bool activated)
    {
        GetComponent<Animator>().SetBool("isActivated", activated);
        isActivated = activated;
    }

    public override void ActivateTab()
    {
        Debug.Log("Ping!");
        SetActivateTab(true);
    }

    public override void DeactivateTab()
    {
        Debug.Log("!gniP");
        SetActivateTab(false);
    }

    private void OnEnable()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        SetActivateTab(!isActivated);
        StartCoroutine(wait());
    }
}
