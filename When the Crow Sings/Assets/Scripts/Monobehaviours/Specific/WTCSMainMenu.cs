using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTCSMainMenu : MonoBehaviour
{
    public GameObject continueGameButton;

    private void OnEnable()
    {
        continueGameButton.SetActive(SaveData.SavedDataExists());
    }
}
