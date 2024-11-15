using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class SaveData
{
    const int saveDataVersion = 1;
    public static Dictionary<string, bool> boolFlags = new Dictionary<string, bool>()
    {
        { "TestingFlag1",false },
        { "TestingFlag2",true },
        { "TestingFlag3",false },
        { "TestingFlag4",true },
        { "SceneStructureFlag", false },

        { "AngelTaskOffered",false },
        { "AngelTaskAccepted",false },
        { "AngelTaskCompleted",false },
        { "AngelBaseCompleted",false },
        { "AngelExhausted",false },

        { "BeauTaskOffered",false },
        { "BeauTaskAccepted",false },
        { "BeauTaskCompleted",false },
        { "BeauBaseCompleted",false },
        { "BeauExhausted",false },
        
        { "CalebTaskOffered",false },
        { "CalebTaskAccepted",false },
        { "CalebTaskCompleted",false },
        { "CalebBaseCompleted",false },
        { "CalebExhausted",false },
        
        { "FaridaIntroduction",false },
        { "FaridaTaskOffered",false },
        { "FaridaTaskAccepted",false },
        { "FaridaTaskCompleted",false },
        { "FaridaBaseCompleted",false },
        { "FaridaExhausted",false },
        
        { "QuinnTaskOffered",false },
        { "QuinnTaskAccepted",false },
        { "QuinnTaskCompleted",false },
        { "QuinnBaseCompleted",false },
        { "QuinnExhausted",false },
        
        { "JazmyneTaskOffered",false },
        { "JazmyneTaskAccepted",false },
        { "JazmyneTaskCompleted",false },
        { "JazmyneBaseCompleted",false },
        { "JazmyneExhausted",false },
        
        { "FranciscoTaskOffered",false },
        { "FranciscoTaskAccepted",false },
        { "FranciscoTaskCompleted",false },
        { "FranciscoBaseCompleted",false },
        { "FranciscoExhausted",false },
        
        { "YuleTaskOffered",false },
        { "YuleTaskAccepted",false },
        { "YuleTaskCompleted",false },
        { "YuleBaseCompleted",false },
        { "YuleExhausted",false },
        
        { "PhilomenaTaskOffered",false },
        { "PhilomenaTaskAccepted",false },
        { "PhilomenaTaskCompleted",false },
        { "PhilomenaBaseCompleted",false },
        { "PhilomenaExhausted",false },
        
        { "TheodoreIntroduction",false },
        { "TheodoreTaskOffered",false },
        { "TheodoreTaskAccepted",false },
        { "TheodoreTaskCompleted",false },
        { "TheodoreBaseCompleted",false },
        { "TheodoreExhausted",false },

        { "RecCenterDoorUnlocked",false },
        { "GreenhouseDoorUnlocked",false },
        { "EnergyHQDoorUnlocked",false},
        { "ClinicDoorUnlocked",false },
        { "Zone1DoorUnlocked",false },
        { "Zone2DoorUnlocked",false },
        { "Zone3DoorUnlocked",false },
        { "Zone4DoorUnlocked",false },

        { "KeyInformation1",false },
        { "KeyInformation2",false },
        { "KeyInformation3",false },

    };

    public static Dictionary<string, int> intFlags = new Dictionary<string, int>()
    {
        { "TestingFlag1",0 },
        { "TestingFlag2",1 },
        { "TestingFlag3",2 },
        { "TestingFlag4",3 },

        {"day",0 },
        {"timeOfDay",0 },
        {"numberOfTasksCompleted",0 },
        {"TBD_OnLoadSpawnPoint_OrSomething",0 },

        {"penguin_cult",0 },
    };

    public static Dictionary<string, string> stringFlags = new Dictionary<string, string>()
    {
        { "TestingFlag1","firstString" },
        { "TestingFlag2","secondString" },
        { "TestingFlag3","thirdString" },
        { "TestingFlag4","fourthString" },

        {"exampleString","myString" },
    };

    public static void SetFlag(string key, bool value)
    {
        boolFlags[key] = value;
        Debug.Log("Key is now "+ boolFlags[key]);
    }
    public static void SetFlag(string key, int value)
    {
        intFlags[key] = value;
        Debug.Log("Key is now " + intFlags[key]);
    }
    public static void SetFlag(string key, string value)
    {
        stringFlags[key] = value;
        Debug.Log("Key is now " + stringFlags[key]);
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

    public static void WriteData()
    {
        switch (saveDataVersion)
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
    public static void ReadData()
    {
        switch (saveDataVersion) // TODO: Make it so it starts reading, stops after the version number, then calls the correct method using this switch statement.
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

    public static IEnumerator EraseData()
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


    public static bool SavedDataExists()
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
        writeInt(saveDataVersion, fileStream);
        // Write the length of boolFlags
        //writeInt(boolFlags.Count, fileStream);
        // Write the length of intFlags
        //writeInt(intFlags.Count, fileStream);
        // Write the length of stringFlags
        //writeInt(stringFlags.Count, fileStream);

        foreach (KeyValuePair<string,bool> i in boolFlags)
        {
            byte valueByte = i.Value ? (byte)1 : (byte)0;
            fileStream.WriteByte(valueByte);
        }
        foreach (KeyValuePair<string,int> i in intFlags)
        {
            writeInt(i.Value, fileStream);
        }
        foreach (KeyValuePair<string,string> i in stringFlags)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(i.Value); //BitConverter.GetBytes(i.Value);
            byte[] valueBytesLengthBytes = BitConverter.GetBytes(valueBytes.Length);
            fileStream.Write(valueBytesLengthBytes,0, valueBytesLengthBytes.Length);
            fileStream.Write(valueBytes, 0, valueBytes.Length);
        }
        fileStream.Close();
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
        foreach (KeyValuePair<string, bool> i in boolFlags)
        {
            tempBoolFlags.Add(i.Key, i.Value);
            tempBoolFlags[i.Key] = fileStream.ReadByte() == 1; //BitConverter.ToBoolean(fileBytes, loop);
            //Debug.Log(tempBoolFlags[i.Key]);
        }
        boolFlags = tempBoolFlags;

        Dictionary<string, int> tempIntFlags = new Dictionary<string, int>();
        foreach (KeyValuePair<string,int> i in intFlags)
        {
            tempIntFlags.Add(i.Key, i.Value);
            tempIntFlags[i.Key] = ReadInt(fileStream);
            //Debug.Log(tempIntFlags[i.Key]);
        }
        intFlags = tempIntFlags;

        Dictionary<string, string> tempStringFlags = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> i in stringFlags)
        {
            tempStringFlags.Add(i.Key, i.Value);
            tempStringFlags[i.Key] = ReadString(fileStream);
            //Debug.Log(tempStringFlags[i.Key]);
        }
        stringFlags = tempStringFlags;

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
