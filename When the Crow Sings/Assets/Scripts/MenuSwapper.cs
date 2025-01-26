using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwapper : MonoBehaviour
{
    public List<GameObject> menus;
    public GameObject journalHolder;

    public void OpenMenu(int whichMenu)
    {
        int currentLoop = 0;
        foreach (var menu in menus)
        {
            if (currentLoop == whichMenu)
                menu.SetActive(true);
            else menu.SetActive(false);

            // TODO: try to figure out the easiest way to put this in a child class maybe so it's not clogging up everything else...
            if (whichMenu > 1) journalHolder.SetActive(true);
            else journalHolder.SetActive(false);
            
            
            currentLoop++;
        }
    }

    public void OpenMenu(GameObject whichMenu)
    {

    }
}
