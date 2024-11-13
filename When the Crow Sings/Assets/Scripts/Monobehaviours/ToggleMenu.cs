using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public GameObject menuOne;
    public GameObject menuTwo;
    private bool isToggled = false;

    private void Awake()
    {
        menuOne.SetActive(true);
        menuTwo.SetActive(false);
    }

    public void OnClicked()
    {
        isToggled = !isToggled;
        menuOne.SetActive(!isToggled);
        menuTwo.SetActive(isToggled);
    }
}
