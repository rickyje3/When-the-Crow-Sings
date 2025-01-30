using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JournalTabSide : JournalTab, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;

    public Animator animator;

    public Image image;


    public bool isActivated = false;
    public override void SetActivateTab(bool activated)
    {
        animator.SetBool("isActivated", activated);
        
        isActivated = activated;
    }

    public override void ActivateTab()
    {
        Debug.Log("Ping!");
        image.sprite = activatedSprite;
        SetActivateTab(true);
    }

    public override void DeactivateTab()
    {
        Debug.Log("!gniP");
        image.sprite = deactivatedSprite;
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

    public List<JournalTabSide> journalTabSides = new List<JournalTabSide>();
    public void OnButtonPressed()
    {
        foreach (JournalTabSide i in journalTabSides)
        {

        }
    }
}
