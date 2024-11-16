using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugIntFlagOption : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI keyText;
    public string key;

    public void Increment()
    {
        SaveData.SetFlag(key, SaveData.intFlags[key]+1);
    }
    public void Decrement()
    {
        SaveData.SetFlag(key, SaveData.intFlags[key] - 1);
    }

    private void Update()
    {
        valueText.text = SaveData.intFlags[key].ToString();
        keyText.text = key;
    }


}
