using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveData
{
    const int saveDataVersion = 0;
    public static Dictionary<string, bool> boolFlags = new Dictionary<string, bool>()
    {
        { "TestingFlag1",false },
        { "TestingFlag2",true },
        { "TestingFlag3",false },
        { "TestingFlag4",true },

        { "AngelTaskOffered",false },
        { "AngelTaskCompleted",false },
        { "AngelBaseComplete",false },
        { "AngelExhausted",false },

        { "BeauTaskOffered",false },
        { "BeauTaskCompleted",false },
        { "BeauBaseComplete",false },
        { "BeauExhausted",false },
        
        { "CalebTaskOffered",false },
        { "CalebTaskCompleted",false },
        { "CalebBaseComplete",false },
        { "CalebExhausted",false },
        
        { "FaridaIntroduction",false },
        { "FaridaTaskOffered",false },
        { "FaridaTaskCompleted",false },
        { "FaridaBaseCompleted",false },
        { "FaridaExhausted",false },
        
        { "QuinnTaskOffered",false },
        { "QuinnTaskCompleted",false },
        { "QuinnBaseCompleted",false },
        { "QuinnExhausted",false },
        
        { "JazmyneTaskOffered",false },
        { "JazmyneTaskCompleted",false },
        { "JazmyneBaseComplete",false },
        { "JazmyneExhausted",false },
        
        { "FranciscoTaskOffered",false },
        { "FranciscoTaskCompleted",false },
        { "FranciscoBaseCompleted",false },
        { "FranciscoExhausted",false },
        
        { "YuleTaskOffered",false },
        { "YuleTaskCompleted",false },
        { "YuleBaseCompleted",false },
        { "YuleExhausted",false },
        
        { "PhilomenaTaskOffered",false },
        { "PhilomenaTaskCompleted",false },
        { "PhilomenaBaseCompleted",false },
        { "PhilomenaExhausted",false },
        
        { "TheodoreIntroduction",false },
        { "TheodoreTaskOffered",false },
        { "TheodoreTaskCompleted",false },
        { "TheodoreBaseCompleted",false },
        { "TheodoreExhausted",false },
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
    }
    public static void SetFlag(string key, int value)
    {
        intFlags[key] = value;
    }
    public static void SetFlag(string key, string value)
    {
        stringFlags[key] = value;
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
                WriteData_V0();
                break;
        }
       
    }
    public static void ReadData()
    {
        switch (saveDataVersion) // TODO: Make it so it starts reading, stops after the version number, then calls the correct method using this switch statement.
        {
            case 0:
                ReadData_V0();
                break;
        }
        
    }


    static void WriteData_V0()
    {
        string filePath = Application.persistentDataPath + "/save.wtcs";

        FileStream fileStream = new FileStream(filePath, FileMode.Create);

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



        //foreach (byte i in fileBytes)
        //{
        //    Debug.Log(i);
        //}

        //FileStream fileStream = new FileStream(filePath, FileMode.Open);
    }
}
