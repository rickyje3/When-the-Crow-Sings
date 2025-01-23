using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HistoryEntry : MonoBehaviour
{
    public int tempINDEXManual = 0;

    public string entryName;
    public string entryDescription;
    public string entryVisibleFlag;
    public string entrySatisfiedFlag;


    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI descriptionTMP;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<DynamicEnable>().associatedDataKey = entryVisibleFlag;
        nameTMP.text = entryName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDescription()
    {
        descriptionTMP.text = entryDescription;
    }
}
