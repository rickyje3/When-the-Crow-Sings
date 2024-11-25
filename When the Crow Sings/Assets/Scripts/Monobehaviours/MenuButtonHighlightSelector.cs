using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonHighlightSelector : MonoBehaviour
{
    public List<GameObject> possibleSelectors;
    public bool exclusive = true;
    public bool selectOnEnable = true;

    private void OnEnable()
    {
        if (selectOnEnable)
            EventSystem.current.SetSelectedGameObject(possibleSelectors[0]);
    }
    private void Update()
    {
        if (exclusive)
        {
            if (!possibleSelectors.Contains(EventSystem.current.currentSelectedGameObject))
                EventSystem.current.SetSelectedGameObject(possibleSelectors[0]);
        }
        else
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                EventSystem.current.SetSelectedGameObject(possibleSelectors[0]);
        }
        foreach (GameObject i in possibleSelectors)
        {
            // if i.isHighlighted, then make it the selected one.
        }
    }
}
