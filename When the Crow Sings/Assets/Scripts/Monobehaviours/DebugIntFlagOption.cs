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
        SaveDataAccess.SetFlag(key, SaveDataAccess.intFlags[key]+1);
    }
    public void Decrement()
    {
        SaveDataAccess.SetFlag(key, SaveDataAccess.intFlags[key] - 1);
    }

    private void Update()
    {
        valueText.text = SaveDataAccess.intFlags[key].ToString();
        keyText.text = key;
    }


}
