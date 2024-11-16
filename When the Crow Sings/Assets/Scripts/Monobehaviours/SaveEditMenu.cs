using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEditMenu : MonoBehaviour
{
    public GameObject intFlagPrefab;
    public GameObject intContentHolder;
    private void Awake()
    {
        float height = 0;
        foreach (KeyValuePair<string, int> i in SaveData.intFlags)
        {
            if (i.Key == "penguin_cult") continue;
            AddIntFlagPrefab(i);
            height += 30;
            intContentHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);
        }
    }

    void AddIntFlagPrefab(KeyValuePair<string,int> i)
    {
        GameObject ii = Instantiate(intFlagPrefab);
        ii.GetComponent<DebugIntFlagOption>().key = i.Key;
        ii.transform.SetParent(intContentHolder.transform, false);
    }
}
