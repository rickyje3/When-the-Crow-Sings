using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugBoolFlagOption : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI keyText;
    public string key;

    public void FlipFlag()
    {
        SaveData.SetFlag(key, !SaveData.boolFlags[key]);
    }

    private void Update()
    {
        valueText.text = SaveData.boolFlags[key].ToString();
        keyText.text = key;
    }


}
