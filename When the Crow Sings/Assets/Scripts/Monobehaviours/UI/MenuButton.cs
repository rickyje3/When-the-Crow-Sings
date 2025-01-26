using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler
{
    [HideInInspector]
    public MenuButtonSelectionHandler menuButtonHighlightSelector;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        menuButtonHighlightSelector.OnPotentialEntered(this);
    }
}
