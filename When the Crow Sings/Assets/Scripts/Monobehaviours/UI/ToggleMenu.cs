using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleMenu : MonoBehaviour
{
    public bool invertMenus = false;

    public GameObject menuOne;
    public GameObject menuOneFocusDefault;
    public GameObject menuTwo;
    public GameObject menuTwoFocusDefault;
    private bool isToggled = false;

    private void Awake()
    {
        if (!invertMenus)
        {
            menuOne.SetActive(true);
            menuTwo.SetActive(false);
            if (menuOneFocusDefault != null) EventSystem.current.SetSelectedGameObject(menuOneFocusDefault);
            isToggled = false;
        }
        else
        {
            menuOne.SetActive(false);
            menuTwo.SetActive(true);
            if (menuTwoFocusDefault != null) EventSystem.current.SetSelectedGameObject(menuOneFocusDefault);
            isToggled = true;
        }
        
    }

    public void OnClicked()
    {
        isToggled = !isToggled;
        menuOne.SetActive(!isToggled);
        menuTwo.SetActive(isToggled);
        if (isToggled)
        {
            if (menuTwoFocusDefault != null) EventSystem.current.SetSelectedGameObject(menuOneFocusDefault);
        }
        else
        {
            if (menuOneFocusDefault != null) EventSystem.current.SetSelectedGameObject(menuOneFocusDefault);
        }
        
    }
}
