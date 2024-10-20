using ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour, IService
{
    public GameObject _playerPrefab;
    public GameObject player = null;

    public GameSignal levelLoadStartSignal;
    public GameSignal levelLoadFinishSignal;

    List<LevelData> currentLevelData = new List<LevelData>();

    const string SCN_PATH = "Assets/Scenes/";

    // ---------------------------------------------------------------------------
    private void Awake() {RegisterSelfAsService();} public void RegisterSelfAsService() {ServiceLocator.Register<GameStateManager>(this);}
    // ---------------------------------------------------------------------------
    private void Start()
    {
        GetLoadedScenes(); // I THINK there was a reason for this to be here??
    }
    private void Update()
    {
        DebugLoadInput(); // Loads individual scenes via keyboard inputs. Hacky implementation of this.
    }
    // ---------------------------------------------------------------------------

    public void OnLoadStart(SignalArguments args)
    {
        if (args.objectArgs[0] is not LevelDataResource) { throw new Exception("No valid LevelDataResource assigned to the load trigger!"); }

        //ServiceLocator.Get<PlayerController>().gameObject.SetActive(false);
        

        

        LoadRoom((LevelDataResource)args.objectArgs[0]);
        //LoadRoom(args.intArgs[0]);
    }
    public void OnLoadFinish(SignalArguments args)
    {
        ValidateScenes();
        if (args.intArgs[0] == 1)
        {
            //ServiceLocator.Get<PlayerController>().gameObject.SetActive(true);
            

            // get all of the spawners, determine which one to use based on which room was left
            FindObjectOfType<PlayerController>().transform.position = FindObjectOfType<PlayerSpawnPoint>().transform.position;
            FindObjectOfType<PlayerController>().movementInput = Vector3.zero;
        }
        
    }

    // ---------------------------------------------------------------------------

    public void LoadRoomDebug(string levelName)
    {
        Destroy(player);

        // Unload previous scenes.
        foreach (Scene i in GetLoadedScenes())
        {
            SceneManager.UnloadSceneAsync(i); //using Async because it yells at me otherwise
            //SceneManager.UnloadScene(i);
        }

        // Check what scenes should be loaded based on save data and exit trigger

        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        player = Instantiate(_playerPrefab);
    }


    public void LoadRoom(LevelDataResource levelDataResource)
    {
        Destroy(player);

        // Unload previous scenes.
        foreach (Scene i in GetLoadedScenes())
        {
            SceneManager.UnloadSceneAsync(i); //using Async because it yells at me otherwise
            //SceneManager.UnloadScene(i);
        }

        // Check what scenes should be loaded based on save data and exit trigger


        // then load them all
        foreach (SceneAsset i in GetScenesToLoad(levelDataResource))
        {
            //SceneManager.LoadScene(i.name, LoadSceneMode.Additive);
            SceneManager.LoadScene(i.name, LoadSceneMode.Additive);
            Debug.Log(i.name + " was loaded!");
        }

        player = Instantiate(_playerPrefab);
    }
    void ValidateScenes()
    {
        currentLevelData = FindObjectsOfType<LevelData>().ToList<LevelData>(); // TODO: Investigate Object.FindObjectByType instead. BY type, not OF type.
        Validate_No_UNASSIGNED();
        Validate_ExactlyOne_LEVEL();
    }


    List<SceneAsset> GetScenesToLoad(LevelDataResource levelDataResource)
    {
        List<SceneAsset> scenes = new List<SceneAsset>();

        scenes.Add(levelDataResource.level);

        if (levelDataResource.subScenes.Count > 0)
        {
            foreach (SubSceneContainer i in levelDataResource.subScenes)
            {
                bool shouldContinue = false;
                foreach (SubSceneLogicBase ii in i.subSceneLogics)
                {
                    if (ii.valueType == SubSceneLogicBase.VALUE_TYPE.BOOL)
                    {
                        bool boolFlag = SaveData.boolFlags[ii.associatedDataKey];
                        if (ii.boolValue != boolFlag)
                        {
                            shouldContinue = true;
                        }
                    }

                    else if (ii.valueType == SubSceneLogicBase.VALUE_TYPE.INT)
                    {
                        int intFlag = SaveData.intFlags[ii.associatedDataKey];
                        //Debug.Log("Flag is == " + intFlag);

                        if (ii.associatedOperator == SubSceneLogicBase.OPERATOR.EQUALS)
                        {
                            if (ii.intValue != intFlag) shouldContinue = true;
                        }
                        else if (ii.associatedOperator == SubSceneLogicBase.OPERATOR.LESS_THAN)
                        {
                            if (ii.intValue! < intFlag) shouldContinue = true;
                        }
                        else
                        {
                            if (ii.intValue! > intFlag) shouldContinue = true;
                        }
                    }
                }
                if (shouldContinue) continue;
                scenes.Add(i.subScene);
            }
        }

        //scenes.Add(SceneManager.GetSceneByBuildIndex(whichTEMP+1));

        return scenes;
    }


    List<Scene> GetLoadedScenes()
    {
        List<Scene> scenes = new List<Scene>();
        foreach (int i in Enumerable.Range(0, SceneManager.sceneCount))
        {
            if (SceneManager.GetSceneAt(i).name != "MainScene")
            {
                scenes.Add(SceneManager.GetSceneAt(i));
            }
        }
        return scenes;
    }

    void Validate_ExactlyOne_LEVEL()
    {
        if (currentLevelData.Count(x => x.sceneType == LevelData.SceneType.LEVEL) != 1)
            throw new System.Exception("Not EXACTLY one LEVEL-type scene is currently loaded!");
        else { Debug.Log("All's well!"); }
    }
    void Validate_No_UNASSIGNED()
    {
        foreach (LevelData i in currentLevelData)
        {
            if (i.sceneType == LevelData.SceneType.UNASSIGNED) throw new System.Exception("Attempting to load a level of type UNASSIGNED!");
        }
    }

    // ---------------------------------------------------------------------------
    void LoadPersistentData() { }
    void SavePersistentData() { }

    // ---------------------------------------------------------------------------

    public List<SceneAsset> debugScenes;
    void DebugLoadInput()
    {
        LevelDataResource testResource = new LevelDataResource();
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            testResource.level = debugScenes[0];
            LoadRoom(testResource);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            testResource.level = debugScenes[1];
            LoadRoom(testResource);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            testResource.level = debugScenes[2];
            LoadRoom(testResource);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            testResource.level = debugScenes[3];
            LoadRoom(testResource);
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            testResource.level = debugScenes[4];
            LoadRoom(testResource);
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            testResource.level = debugScenes[5];
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            testResource.level = debugScenes[6];
        }
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            testResource.level = debugScenes[7];
        }
        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            testResource.level = debugScenes[8];
        }
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            testResource.level = debugScenes[9];
        }
        if (Input.GetKeyUp(KeyCode.Minus))
        {
            testResource.level = debugScenes[10];
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            testResource.level = debugScenes[11];
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            testResource.level = debugScenes[12];
        }
        
    }
}
