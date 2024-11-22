using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleMenu : MonoBehaviour
{
    public GameObject menuOne;
    public GameObject menuOneFocusDefault;
    public GameObject menuTwo;
    public GameObject menuTwoFocusDefault;
    private bool isToggled = false;

    private void Awake()
    {
        menuOne.SetActive(true);
        menuTwo.SetActive(false);
        if (menuOneFocusDefault != null) EventSystem.current.SetSelectedGameObject(menuOneFocusDefault);
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
