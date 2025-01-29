using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class JournalTabSide : JournalTab, IPointerEnterHandler, IPointerExitHandler
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
        //StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        SetActivateTab(!isActivated);
        StartCoroutine(wait());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ActivateTab();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DeactivateTab();
    }
}
