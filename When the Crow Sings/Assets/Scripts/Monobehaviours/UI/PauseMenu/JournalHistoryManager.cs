using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JournalHistoryManager : MonoBehaviour
{
    public GameObject contentHolder;
    public List<HistoryEntry> historyEntries = new List<HistoryEntry>();
    //List<int> historyEntriesOrder = new List<int>();

    List<string> _associatedDataKeys = new List<string>();

    Dictionary<string,bool> _associatedData = new Dictionary<string,bool>();

    private void OnEnable()
    {
        historyEntries = contentHolder.GetComponentsInChildren<HistoryEntry>().ToList();
        AddHistoryOrderToSaveDataIfNewGame();

    }

    private void OrganizeHistoryEntriesBySaveData()
    {
        int lowestIndexSoFar = 999999999;
        for (int i = 0; i < SaveDataAccess.saveData.historyEntriesOrder.Count; i++)
        {
            if (SaveDataAccess.saveData.historyEntriesOrder[i] < lowestIndexSoFar)
                MoveHistoryEntryToStart(i);
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
        int currentLoop = 0;
        foreach (KeyValuePair<string, bool> i in _associatedData)
        {
            KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>(i.Key, SaveDataAccess.saveData.boolFlags[i.Key]);
            if (i.Value != currentPair.Value)
            {
                Debug.Log("History entry order doesn't match! Resorting!");
                MoveHistoryEntryToStart(currentLoop);

                // Reflect the change in the historyentriesorder.
                int _orderIndex = SaveDataAccess.saveData.historyEntriesOrder[currentLoop];
                SaveDataAccess.saveData.historyEntriesOrder.RemoveAt(currentLoop);
                SaveDataAccess.saveData.historyEntriesOrder.Insert(0, _orderIndex);

            }

            _associatedData[i.Key] = currentPair.Value;

            currentLoop++;
        }
    }

    private void MoveHistoryEntryToStart(int _entryToMove)
    {
        historyEntries[_entryToMove].transform.SetAsFirstSibling();
        HistoryEntry _entry = historyEntries[_entryToMove];
        historyEntries.RemoveAt(_entryToMove);
        historyEntries.Insert(0, _entry);
    }
}
