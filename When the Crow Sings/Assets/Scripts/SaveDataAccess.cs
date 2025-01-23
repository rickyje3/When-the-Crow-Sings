using ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
public class SaveDataAccess
{
    static public SaveData saveData = new SaveData();
    static public List<string> keysToTriggerPopup = new List<string> { "TestingFlag1" };

    static public GameSignal popupUpdateMessage;
    public static void SetFlag(string key, bool value)
    {
        saveData.boolFlags[key] = value;
        Debug.Log(key + " is now "+ saveData.boolFlags[key]);


        if (saveData.boolFlags["FlowerOne"] && saveData.boolFlags["FlowerTwo"] && saveData.boolFlags["FlowerThree"]) saveData.boolFlags["FranciscoTaskCompleted"] = true;
        if (saveData.boolFlags["FlowerOne"] && saveData.boolFlags["FlowerTwo"] && saveData.boolFlags["FlowerThree"]) saveData.boolFlags["MFFranciscoTC"] = true;
        if (saveData.boolFlags["FlowerOne"] && saveData.boolFlags["FlowerTwo"] && saveData.boolFlags["FlowerThree"]) saveData.boolFlags["FranciscoTaskOn"] = false;

        if (saveData.boolFlags["YuleString"] && saveData.boolFlags["YuleRod"] && saveData.boolFlags["YuleString"]) saveData.boolFlags["YuleTaskPartCompleted"] = true;
        if (saveData.boolFlags["YuleString"] && saveData.boolFlags["YuleRod"] && saveData.boolFlags["YuleString"]) saveData.boolFlags["MFYuleTPC"] = true;
        if (saveData.boolFlags["YuleString"] && saveData.boolFlags["YuleRod"] && saveData.boolFlags["YuleString"]) saveData.boolFlags["YuleTaskOneOn"] = false;

        if (saveData.boolFlags["BeauBaseCompleted"] && saveData.boolFlags["FranciscoBaseCompleted"]) saveData.boolFlags["ifBeauAndFrancisco"] = true;
        if (saveData.boolFlags["BeauBaseCompleted"] && saveData.boolFlags["AngelBaseCompleted"]) saveData.boolFlags["ifBeauAndAngel"] = true;
        if (saveData.boolFlags["FaridaBaseCompleted"] && saveData.boolFlags["CalebBaseCompleted"]) saveData.boolFlags["ifFaridaAndCaleb"] = true;
        if (saveData.boolFlags["CalebBaseCompleted"] && saveData.boolFlags["AngelBaseCompleted"]) saveData.boolFlags["ifCalebAndAngel"] = true;
        //else boolFlags["FranciscoTaskCompleted"] = false; // Commented out rn because for testing we only have 1 QTE.


    }
    public static void SetFlag(string key, int value)
    {
        saveData.intFlags[key] = value;
        if (saveData.intFlags["timeOfDay"] > 3) saveData.intFlags["timeOfDay"] = 1;
        Debug.Log(key + " is now " + saveData.intFlags[key]);
    }
    public static void SetFlag(string key, string value)
    {
        saveData.stringFlags[key] = value;
        Debug.Log(key + " is now " + saveData.stringFlags[key]);

        if (keysToTriggerPopup.Contains(key) && value== "true")
        {
            popupUpdateMessage.Emit();
        }
    }

    //public static bool GetFlag<Bool>(string key)
    //{
    //    return boolFlags[key];
    //}
    //public static int GetFlag<Int>(string key)
    //{
    //    return intFlags[key];
    //}
    //public static string GetFlag<String>(string key)
    //{
    //    return stringFlags[key];
    //}

    static void writeInt(int integer, FileStream fileStream)
    {
        byte[] valueBytes = BitConverter.GetBytes(integer);
        fileStream.Write(valueBytes, 0, valueBytes.Length);
    }

    public static void WriteDataToDisk()
    {
        switch (saveData.saveDataVersion)
        {
            case 0:
#pragma warning disable CS0162 // Unreachable code detected
                WriteData_V0();
                break;
            default:
                WriteData_V0();
                break;
#pragma warning restore CS0162 // Unreachable code detected
        }

    }
    public static void ReadDataFromDisk()
    {
        ResetSaveData();

        switch (saveData.saveDataVersion) // TODO: Make it so it starts reading, stops after the version number, then calls the correct method using this switch statement.
        {
            case 0:
#pragma warning disable CS0162 // Unreachable code detected
                ReadData_V0();
                break;
            default:
                ReadData_V0();
                break;
#pragma warning restore CS0162 // Unreachable code detected
        }
        Debug.Log("Data read!");
    }

    public static IEnumerator EraseDataFromDisk()
    {
        string filePath = Application.persistentDataPath + "/save.wtcs";

        while (true)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Debug.Log("File deleted successfully.");
                }
                yield break; // Exit once deletion is successful
            }
            catch (IOException)
            {
                Debug.LogWarning("File is in use by another process, retrying...");
            }
            yield return new WaitForSeconds(0.1f); // Retry after a delay
        }
    }

    public static void ResetSaveData()
    {
        saveData = new SaveData();
    }


    public static bool SavedDataExistsOnDisk()
    {
        if (File.Exists(Application.persistentDataPath + "/save.wtcs"))
        {
            return true;
        }
        Debug.Log("No save data exists on disk!");
        return false;
    }

    static void WriteData_V0()
    {
        //EraseData();

        string filePath = Application.persistentDataPath + "/save.wtcs";

        FileStream fileStream;
        //if (!File.Exists(filePath))
        //{
        //    fileStream = new FileStream(filePath, FileMode.Create);
        //}
        //else
        //{
        //    fileStream = new FileStream(filePath, FileMode.Truncate);
        //}
        fileStream = new FileStream(filePath, FileMode.Create);

        // Write the save data version.
        writeInt(saveData.saveDataVersion, fileStream);
        // Write the length of boolFlags
        //writeInt(boolFlags.Count, fileStream);
        // Write the length of intFlags
        //writeInt(intFlags.Count, fileStream);
        // Write the length of stringFlags
        //writeInt(stringFlags.Count, fileStream);

        foreach (KeyValuePair<string,bool> i in saveData.boolFlags)
        {
            byte valueByte = i.Value ? (byte)1 : (byte)0;
            fileStream.WriteByte(valueByte);
        }
        foreach (KeyValuePair<string,int> i in saveData.intFlags)
        {
            writeInt(i.Value, fileStream);
        }
        foreach (KeyValuePair<string,string> i in saveData.stringFlags)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(i.Value); //BitConverter.GetBytes(i.Value);
            byte[] valueBytesLengthBytes = BitConverter.GetBytes(valueBytes.Length);
            fileStream.Write(valueBytesLengthBytes,0, valueBytesLengthBytes.Length);
            fileStream.Write(valueBytes, 0, valueBytes.Length);
        }
        fileStream.Close();
        Debug.Log("File saved to disk!");
    }

    static int ReadInt(FileStream fileStream)
    {
        byte[] intBytes = new byte[4];
        fileStream.Read(intBytes, 0, 4);
        return BitConverter.ToInt32(intBytes, 0);
    }
    static string ReadString(FileStream fileStream)
    {
        int stringLength = ReadInt(fileStream);

        byte[] stringBytes = new byte[stringLength];
        fileStream.Read(stringBytes,0,stringLength);
        return Encoding.UTF8.GetString(stringBytes);
    }
    static void ReadData_V0()
    {
        string filePath = Application.persistentDataPath + "/save.wtcs";
        //byte[] fileBytes = File.ReadAllBytes(filePath);
        FileStream fileStream = new FileStream(filePath, FileMode.Open);

        //int loadedSaveDataVersion = fileBytes[loop];
        
        int loadedSaveDataVersion = ReadInt(fileStream);
        //Debug.Log("Save data version is "+loadedSaveDataVersion);

        Dictionary<string,bool> tempBoolFlags = new Dictionary<string,bool>();
        foreach (KeyValuePair<string, bool> i in saveData.boolFlags)
        {
            tempBoolFlags.Add(i.Key, i.Value);
            tempBoolFlags[i.Key] = fileStream.ReadByte() == 1; //BitConverter.ToBoolean(fileBytes, loop);
            //Debug.Log(tempBoolFlags[i.Key]);
        }
        saveData.boolFlags = tempBoolFlags;

        Dictionary<string, int> tempIntFlags = new Dictionary<string, int>();
        foreach (KeyValuePair<string,int> i in saveData.intFlags)
        {
            tempIntFlags.Add(i.Key, i.Value);
            tempIntFlags[i.Key] = ReadInt(fileStream);
            //Debug.Log(tempIntFlags[i.Key]);
        }
        saveData.intFlags = tempIntFlags;

        Dictionary<string, string> tempStringFlags = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> i in saveData.stringFlags)
        {
            tempStringFlags.Add(i.Key, i.Value);
            tempStringFlags[i.Key] = ReadString(fileStream);
            //Debug.Log(tempStringFlags[i.Key]);
        }
        saveData.stringFlags = tempStringFlags;

        fileStream.Close();

        //foreach (byte i in fileBytes)
        //{
        //    Debug.Log(i);
        //}

        //FileStream fileStream = new FileStream(filePath, FileMode.Open);
    }


    private static void PenguinCultAttemptsToScheduleAMeeting()
    {
        SetFlag("penguin_cult",UnityEngine.Random.Range(0,623));
    }
}
