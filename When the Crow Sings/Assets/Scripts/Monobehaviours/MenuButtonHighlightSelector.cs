using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonHighlightSelector : MonoBehaviour//, IPointerEnterHandler
{
    public List<MenuButtonDetector> possibleSelectors;
    public bool exclusive = true;
    public bool selectOnEnable = true;

    private MenuButtonDetector lastSelected = null; // Only includes the lastSelected if it's part of the possibleSelectors, unlike the built-in one.

    private void SetSelectedGameObject(GameObject newSelected)
    {
        EventSystem.current.SetSelectedGameObject(newSelected);
    }
    public void OnPotentialEntered(MenuButtonDetector menuButtonDetector)
    {
        if (possibleSelectors.Contains(menuButtonDetector))
            SetSelectedGameObject(menuButtonDetector.gameObject);
    }
    private void OnEnable()
    {
        foreach (var possibleSelector in possibleSelectors)
        {
            possibleSelector.menuButtonHighlightSelector = this;
        }
        if (selectOnEnable)
            SetSelectedGameObject(possibleSelectors[0].gameObject);
    }
    private void OnDisable()
    {
        lastSelected = null;
    }
    private void Update()
    {
        if (exclusive)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                SetLastSelectedOrFirstPossibleSelector();
            }
            else if (!possibleSelectors.Contains(EventSystem.current.currentSelectedGameObject.GetComponent<MenuButtonDetector>()))
                    SetSelectedGameObject(possibleSelectors[0].gameObject);
            
        }
        else
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                SetLastSelectedOrFirstPossibleSelector();
        }

        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<MenuButtonDetector>())
            lastSelected = EventSystem.current.currentSelectedGameObject.GetComponent<MenuButtonDetector>();
    }

    private void SetLastSelectedOrFirstPossibleSelector()
    {
        if (lastSelected != null) SetSelectedGameObject(lastSelected.gameObject);
        else SetSelectedGameObject(possibleSelectors[0].gameObject);
    }
}
