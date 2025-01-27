using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public int saveDataVersion = 1;
    public Dictionary<string, bool> boolFlags = new Dictionary<string, bool>()
    {
        //Dialogue and task related flags
        { "TestingFlag1",false },
        { "TestingFlag2",true },
        { "TestingFlag3",false },
        { "TestingFlag4",true },
        { "SceneStructureFlag",false  },

        { "IntroContextSeen",false },

        { "AngelTaskOffered",false },
        { "AngelTaskAccepted",false },
        { "AngelTaskCompleted",false },
        { "AngelTaskOn",false },
        { "AngelBaseCompleted",false },
        { "AngelExhausted",false },

        { "BeauTaskOffered",false },
        { "BeauTaskAccepted",false },
        { "BeauTaskCompleted",false },
        { "BeauTaskOn",false },
        { "BeauBaseCompleted",false },
        { "BeauExhausted",false },

        { "CalebTaskOffered",false },
        { "CalebTaskAccepted",false },
        { "CalebTaskCompleted",false },
        { "CalebTaskOn",false },
        { "CalebBaseCompleted",false },
        { "CalebExhausted",false },

        { "FaridaIntroduction",false },
        { "FaridaTaskOffered",false },
        { "FaridaTaskAccepted",false },
        { "FaridaTaskCompleted",false },
        { "FaridaTaskOn",false },
        { "FaridaBaseCompleted",false },
        { "FaridaExhausted",false },

        { "QuinnTaskOffered",false },
        { "QuinnTaskAccepted",false },
        { "QuinnTaskCompleted",false },
        { "QuinnTaskOn",false },
        { "QuinnBaseCompleted",false },
        { "QuinnExhausted",false },

        { "JazmyneTaskOffered",false },
        { "JazmyneTaskAccepted",false },
        { "JazmyneTaskCompleted",false },
        { "JazmyneTaskOn",false },
        { "PapersUp",false },
        { "PapersDown",false },
        { "QTEOn",false },
        { "JazmyneBaseCompleted",false },
        { "JazmyneExhausted",false },

        { "FranciscoTaskOffered",false },
        { "FranciscoTaskAccepted",false },
        { "FranciscoTaskCompleted",false },
        { "FranciscoTaskOn",false },
        { "FlowerOne",false },
        { "FlowerTwo",false },
        { "FlowerThree",false },
        { "FranciscoBaseCompleted",false },
        { "FranciscoExhausted",false },

        { "YuleTaskOffered",false },
        { "YuleTaskAccepted",false },
        { "YuleTaskPartCompleted",false },
        { "YuleTaskCompleted",false },
        { "YuleTaskOneOn",false },
        { "YuleTaskTwoOn",false },
        { "YuleRod",false },
        { "YuleString",false },
        { "YuleHook",false },
        { "YuleBaseCompleted",false },
        { "YuleExhausted",false },

        { "PhilomenaTaskOffered",false },
        { "PhilomenaTaskAccepted",false },
        { "PhilBatt1",false },
        { "PhilBatt2",false },
        { "PhilCasette",false },
        { "PhilomenaTaskPartCompleted",false },
        { "PhilomenaTaskCompleted",false },
        { "PhilomenaTaskOn",false },
        { "PhilomenaBaseCompleted",false },
        { "PhilomenaExhausted",false },

        { "TheodoreIntroduction",false },
        { "TheodoreTaskOffered",false },
        { "TheodoreTaskAccepted",false },
        { "RubiksCube",false },
        { "PaperClip",false },
        { "Thumbdrive",false },
        { "TheodoreTaskCompleted",false },
        { "TheodoreTaskOn",false },
        { "TheodoreBaseCompleted",false },
        { "TheodoreExhausted",false },

        { "ifBeauAndFrancisco",false },
        { "ifBeauAndAngel",false },
        { "ifFaridaAndCaleb",false },
        { "ifCalebAndAngel",false },

        { "SeenBrokenGauges",false },
        { "SeenDoor",false },

        //Door/loadzone relevant flags

        { "RecCenterDoorUnlocked",false },
        { "GreenhouseDoorUnlocked",false },
        { "EnergyHQDoorUnlocked",false },
        { "ClinicDoorUnlocked",false },
        { "Zone1DoorUnlocked",false },
        { "WoodPileMovable",false },
        { "Zone2DoorUnlocked",false },
        { "Zone3DoorUnlocked",false },
        { "Zone4DoorUnlocked",false },
        { "TheoPickaxe",false },
        { "TheodoreHole",false },

        //Endings relevant flags

        { "KeyInformation1",false },
        { "KeyInformation2",false },
        { "KeyInformation3",false },

        //Map relevant flags
        { "MFAngelTA",false },
        { "MFAngelTC",false },

        { "MFBeauTA",false },
        { "MFBeauTC",false },

        { "MFCalebTA",false },
        { "MFCalebTC",false },

        { "MFJazmyneTag",false },
        { "MFJazmyneTA",false },
        { "MFJazmyneTPC",false },
        { "MFJazmyneTC",false },

        { "MFQuinnTag",false },
        { "MFQuinnTA",false },
        { "MFQuinnTC",false },

        { "MFFranciscoTag",false },
        { "MFFranciscoTA",false },
        { "MFFranciscoTC",false },

        { "MFFaridaTag",false },
        { "MFFaridaTA",false },
        { "MFFaridaTC",false },

        { "MFTheodoreTag",false },
        { "MFTheodorePT",false },
        { "MFTheodoreTPC",false },
        { "MFTheodoreGI",false },
        { "MFTheodoreTA",false },
        { "MFTheodoreTC",false },

        { "MFYuleTag",false },
        { "MFYuleTA",false },
        { "MFYuleRod",false },
        { "MFYuleString",false },
        { "MFYuleHook",false },
        { "MFYuleTPC",false },
        { "MFYuleTNC",false },
        { "MFYuleTC",false },

        { "MFPhilomenaTag",false },
        { "MFPhilomenaTA",false },
        { "MFPhilomenaBatt1",false },
        { "MFPhilomenaBatt2",false },
        { "MFPhilomenaCasette",false },
        { "MFPhilomenaTPC",false },
        { "MFPhilomenaTC",false },

        //History relevant flags -- These NEVER get flipped off
        { "HFAngelTA",false },
        { "HFAngelTC",false },
        { "HFAngelBC",false },

        { "HFBeauTA",false },
        { "HFBeauTC",false },
        { "HFBeauBC",false },

        { "HFCalebTA",false },
        { "HFCalebTC",false },
        { "HFCalebBC",false },

        { "HFJazmyneTA",false },
        { "HFJazmyneTPC",false },
        { "HFJazmyneTC",false },
        { "HFJazmyneBC",false },

        { "HFQuinnTA",false },
        { "HFQuinnTC",false },
        { "HFQuinnBC",false },

        { "HFFranciscoTA",false },
        { "HFFranciscoTC",false },
        { "HFFranciscoBC",false },

        { "HFFaridaTA",false },
        { "HFFaridaTC",false },
        { "HFFaridaBC",false },

        { "HFTheodorePT",false },
        { "HFTheodoreTPC",false },
        { "HFTheodoreGI",false },
        { "HFTheodoreTA",false },
        { "HFTheodoreTC",false },
        { "HFTheodoreBC",false },

        { "HFYuleTA",false },
        { "HFYuleRod",false },
        { "HFYuleString",false },
        { "HFYuleHook",false },
        { "HFYuleTPC",false },
        { "HFYuleTNC",false },
        { "HFYuleTC",false },
        { "HFYuleBC",false },

        { "HFPhilomenaTA",false },
        { "HFPhilomenaBatt1",false },
        { "HFPhilomenaBatt2",false },
        { "HFPhilomenaCasette",false },
        { "HFPhilomenaTPC",false },
        { "HFPhilomenaTC",false },
        { "HFPhilomenaBC",false },
    };

    public Dictionary<string, int> intFlags = new Dictionary<string, int>()
    {
        { "TestingFlag1",0 },
        { "TestingFlag2",1 },
        { "TestingFlag3",2 },
        { "TestingFlag4",3 },

        {"day",1 },
        {"timeOfDay",1 },
        {"numberOfTasksCompleted",0 },
        {"TBD_OnLoadSpawnPoint_OrSomething",0 },

        {"penguin_cult",623 },
        {"benchcounter",0 },
        {"PhilTalk", 0},
        {"theodoor", 0},

        {"levelDataIndex", 1},
    };

    public Dictionary<string, string> stringFlags = new Dictionary<string, string>()
    {
        { "TestingFlag1","firstString" },
        { "TestingFlag2","secondString" },
        { "TestingFlag3","thirdString" },
        { "TestingFlag4","fourthString" },

        {"exampleString","myString" },
    };
}
