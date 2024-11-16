using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveEditMenu : MonoBehaviour
{
    public GameObject intFlagPrefab;
    public GameObject intContentHolder;
    public GameObject boolFlagPrefab;
    public GameObject boolContentHolder;
    private void Awake()
    {
        float boolHeight = 0;
        float additionalSpacing = 30 + boolContentHolder.GetComponent<VerticalLayoutGroup>().spacing;

        foreach (KeyValuePair<string, bool> i in SaveData.boolFlags)
        {
            AddBoolFlagPrefab(i);
            boolHeight += additionalSpacing;
        }
        boolContentHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, boolHeight);
        
        float intHeight = 0;
        foreach (KeyValuePair<string, int> i in SaveData.intFlags)
        {
            if (i.Key == "penguin_cult") continue;
            AddIntFlagPrefab(i);
            intHeight += additionalSpacing;
        }
        intContentHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, intHeight);
    }

    void AddIntFlagPrefab(KeyValuePair<string,int> i)
    {
        GameObject ii = Instantiate(intFlagPrefab);
        ii.GetComponent<DebugIntFlagOption>().key = i.Key;
        ii.transform.SetParent(intContentHolder.transform, false);
    }
    void AddBoolFlagPrefab(KeyValuePair<string,bool> i)
    {
        GameObject ii = Instantiate(boolFlagPrefab);
        ii.GetComponent<DebugBoolFlagOption>().key = i.Key;
        ii.transform.SetParent(boolContentHolder.transform, false);
    }
}
