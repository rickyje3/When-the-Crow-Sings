using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDebugMenu : MonoBehaviour
{
    public GameObject debugMenu;
    public GameObject RealMenu;
    private bool isToggled = false;

    private void Awake()
    {
        debugMenu.SetActive(true);
        RealMenu.SetActive(false);
    }

    public void OnClicked()
    {
        isToggled = !isToggled;
        debugMenu.SetActive(!isToggled);
        RealMenu.SetActive(isToggled);
    }
}
