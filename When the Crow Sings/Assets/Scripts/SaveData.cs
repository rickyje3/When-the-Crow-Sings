using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveData
{
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

    
    public static void WriteData()
    {
        string filePath = Application.persistentDataPath + "/save.wtcs";

        FileStream fileStream = new FileStream(filePath, FileMode.Create);

        int offset = 0;
        foreach (KeyValuePair<string,bool> i in boolFlags)
        {
            byte valueByte = i.Value ? (byte)1 : (byte)0;

            fileStream.WriteByte(valueByte);
            //Debug.Log(valueByte);
            offset++;
        }


        fileStream.Close();
    }
    public static void ReadData()
    {
        string filePath = Application.persistentDataPath + "/save.wtcs";
        byte[] fileBytes = File.ReadAllBytes(filePath);

        foreach (byte i in fileBytes)
        {
            Debug.Log(i);
        }

        //FileStream fileStream = new FileStream(filePath, FileMode.Open);
    }
}
