using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonDetector : MonoBehaviour, IPointerEnterHandler
{
    [HideInInspector]
    public MenuButtonHighlightSelector menuButtonHighlightSelector;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        menuButtonHighlightSelector.OnPotentialEntered(this);
    }
}
