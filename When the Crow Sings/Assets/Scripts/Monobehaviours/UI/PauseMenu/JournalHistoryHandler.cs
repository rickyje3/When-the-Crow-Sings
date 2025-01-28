using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class JournalHistoryHandler : MonoBehaviour
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

    private void OnEnable()
    {
        InputManager.playerInputActions.UI.Journal_Scroll.performed += OnJournalScrollPerformed;
    }
    private void OnDisable()
    {
        InputManager.playerInputActions.UI.Journal_Scroll.performed -= OnJournalScrollPerformed;
    }

    void OnJournalScrollPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Scroll scroll scroll your boat...");// + context.ReadValue<float>().ToString());
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
        Dictionary<string, bool> currentData = new Dictionary<string, bool>();

        List<HistoryEntry> historyEntriesToMove = new List<HistoryEntry>();

        for(int i = 0; i < historyEntries.Count; i++)
        {
            //KeyValuePair<string, bool> currentPair = new KeyValuePair<string, bool>(i.Key, SaveDataAccess.saveData.boolFlags[i.Key]);
            string current_key = historyEntries[i].associatedDataKey_EnableEntry;
            bool previous_value = _associatedData[current_key];
            bool new_value = SaveDataAccess.saveData.boolFlags[current_key];


            if (previous_value != new_value)
            {
                //Debug.Log("Loop " + i.ToString() + " does not match save data.");

                historyEntries[i].gameObject.SetActive(new_value);
                historyEntriesToMove.Add(historyEntries[i]);

                // Reflect the change in the historyentriesorder.
                int _orderIndex = SaveDataAccess.saveData.historyEntriesOrder[i];
                SaveDataAccess.saveData.historyEntriesOrder.RemoveAt(i);
                SaveDataAccess.saveData.historyEntriesOrder.Insert(0, _orderIndex);

            }

            currentData.Add(current_key, new_value);
        }

        for (int i = 0; i < historyEntriesToMove.Count; i++)
        {
            MoveHistoryEntryToStart(historyEntries.IndexOf(historyEntriesToMove[i]));
        }

        _associatedData = currentData;
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
