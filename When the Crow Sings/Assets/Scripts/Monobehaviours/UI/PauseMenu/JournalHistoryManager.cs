using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JournalHistoryManager : MonoBehaviour
{
    public GameObject contentHolder;

    [HideInInspector]
    public List<HistoryEntry> historyEntries = new List<HistoryEntry>();

    Dictionary<string,bool> _associatedData = new Dictionary<string,bool>();

    private void Awake() // Using awake because this needs to happen regardless of being Enabled.
    {
        populateHistoryEntries();

        AddHistoryOrderToSaveDataIfNewGame();

        // Populate the associatedData so that it can be used in Update().  Also set all entries inactive so they don't appear at the beginning.
        foreach (HistoryEntry entry in historyEntries)
        {
            _associatedData.Add(entry.associatedDataKey_EnableEntry, false);
            entry.gameObject.SetActive(false);
        }
    }

    void populateHistoryEntries()
    {
        historyEntries = contentHolder.GetComponentsInChildren<HistoryEntry>(true).ToList();
    }

    private void OrganizeHistoryEntriesBySaveData()
    {
        int lowestIndexSoFar = 999999999;
        for (int i = 0; i < SaveDataAccess.saveData.historyEntriesOrder.Count; i++)
        {
            if (SaveDataAccess.saveData.historyEntriesOrder[i] < lowestIndexSoFar)
            {
                MoveHistoryEntryToStart(i);
                lowestIndexSoFar = SaveDataAccess.saveData.historyEntriesOrder[i];
            }
                

        }
    }

    private void Update()
    {
        CheckOrderOfEntriesAgainstSaveData();
    }

    private void AddHistoryOrderToSaveDataIfNewGame()
    {
        if (SaveDataAccess.saveData.historyEntriesOrder == null || SaveDataAccess.saveData.historyEntriesOrder.Count <= 0)
        {
            SaveDataAccess.saveData.historyEntriesOrder = new List<int>();
            for (int i = 0; i < historyEntries.Count; i++)
            {
                SaveDataAccess.saveData.historyEntriesOrder.Add(i);
            }

            OrganizeHistoryEntriesBySaveData();
        }
    }

    

    private void CheckOrderOfEntriesAgainstSaveData()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Key pressed.");

            Dictionary<string, bool> currentData = new Dictionary<string, bool>();

            int currentLoop = 0;

            List<HistoryEntry> historyEntriesToMove = new List<HistoryEntry>();

            foreach (KeyValuePair<string, bool> i in _associatedData)
            {
                KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>(i.Key, SaveDataAccess.saveData.boolFlags[i.Key]);
                if (i.Value != currentPair.Value)
                {
                    Debug.Log("Loop " + currentLoop.ToString() + " does not match save data.");

                    historyEntries[currentLoop].gameObject.SetActive(currentPair.Value);
                    historyEntriesToMove.Add(historyEntries[currentLoop]);

                    // Reflect the change in the historyentriesorder.
                    int _orderIndex = SaveDataAccess.saveData.historyEntriesOrder[currentLoop];
                    SaveDataAccess.saveData.historyEntriesOrder.RemoveAt(currentLoop);
                    SaveDataAccess.saveData.historyEntriesOrder.Insert(0, _orderIndex);

                }

                currentData.Add(i.Key, i.Value);

                currentLoop++;
            }

            for (int i = 0; i < historyEntriesToMove.Count; i++)
            {
                MoveHistoryEntryToStart(historyEntries.IndexOf(historyEntriesToMove[i]));
            }

            _associatedData = currentData;
        }

        
    }



    private void MoveHistoryEntryToStart(int _entryToMove)
    {
        historyEntries[_entryToMove].transform.SetAsFirstSibling();
        populateHistoryEntries();
        //HistoryEntry _entry = historyEntries[_entryToMove];
        //historyEntries.RemoveAt(_entryToMove);
        //historyEntries.Insert(0, _entry);
    }
}
