using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempHistoryScript : MonoBehaviour
{
    public List<HistoryEntry> historyEntries = new List<HistoryEntry>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //int indexThing = 100;
        //HistoryEntry currentTopEntry = null;
        //foreach (HistoryEntry entry in historyEntries)
        //{
        //    if (entry.tempINDEXManual < indexThing && entry.isActiveAndEnabled)
        //    {
        //        indexThing = entry.tempINDEXManual;
        //        currentTopEntry = entry;
        //    }
        //}
        //if (currentTopEntry != null)
        //{
        //    currentTopEntry.descriptionTMP.text = currentTopEntry.entryDescription;
        //}
    }
}
