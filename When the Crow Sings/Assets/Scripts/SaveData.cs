using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveData
{
    public static Dictionary<string, bool> boolFlags = new Dictionary<string, bool>()
    {
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
        {"day",0 },
        {"timeOfDay",0 },
        {"numberOfTasksCompleted",0 },
        {"TBD_OnLoadSpawnPoint_OrSomething",0 },
    };
}
