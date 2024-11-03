using ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;

public class GameStateManager : MonoBehaviour, IService
{
    public MainMenuDebugLoadHolder mainMenuDebugLoadHolder;

    public GameObject _playerPrefab;
    public GameObject playerHolder = null;
    public GameObject playerContent = null;

    public GameSignal levelLoadStartSignal;
    public GameSignal levelLoadFinishSignal;

    private int targetSpawnIndex = 0;
    private bool canLoad = true;

    List<LevelData> currentLevelData = new List<LevelData>();

    const string SCN_PATH = "Assets/Scenes/";

    // ---------------------------------------------------------------------------
    private void Awake() {RegisterSelfAsService();} public void RegisterSelfAsService() {ServiceLocator.Register<GameStateManager>(this);}
    // ---------------------------------------------------------------------------
    private void Start()
    {
        GetLoadedScenes(); // I THINK there was a reason for this to be here??

        //SaveData.ReadData();
        if (mainMenuDebugLoadHolder.resourceToLoad != null)
        {
            LoadRoom(mainMenuDebugLoadHolder.resourceToLoad);
        }
        
    }
    private void Update()
    {
        DebugLoadInput(); // Loads individual scenes via keyboard inputs. Hacky implementation of this.
    }
    // ---------------------------------------------------------------------------

    public void OnLoadStart(SignalArguments args)
    {
        if (canLoad)
        {
            if (args.objectArgs[0] is not LevelDataResource) { throw new Exception("No valid LevelDataResource assigned to the load trigger!"); }

            targetSpawnIndex = args.intArgs[0];
            LoadRoom((LevelDataResource)args.objectArgs[0]);
        }
    }
    public void OnLoadFinish(SignalArguments args)
    {
        ValidateScenes();
        if (args.intArgs[0] == 1) // If this signal was sent by a LEVEL being loaded
        {
            SpawnPlayer();
        }
    }

    // ---------------------------------------------------------------------------

    void SpawnPlayer()
    {
        
        PlayerSpawnPoint spawnPoint = null;
        List<PlayerSpawnPoint> spawnPointsWithMatchingIndex = new List<PlayerSpawnPoint>();
        foreach (PlayerSpawnPoint i in FindObjectsOfType<PlayerSpawnPoint>())
        {
            if (i.entranceIndex == targetSpawnIndex)
            {
                //spawnPoint = i;
                spawnPointsWithMatchingIndex.Add(i);
            }
        }
        if (spawnPointsWithMatchingIndex.Count > 1) throw new System.Exception("More than one spawn point found with the same index.");
        if (spawnPointsWithMatchingIndex.Count < 1) throw new System.Exception("Error! No spawn point found that matches the desired index!");
        spawnPoint = spawnPointsWithMatchingIndex[0];

        playerHolder = Instantiate(_playerPrefab);
        playerContent = playerHolder.GetComponent<PlayerHolder>().playerContent;

        playerContent.transform.position = spawnPoint.transform.position;
        playerContent.GetComponent<CharacterController>().enabled = true;
    }


    public void LoadRoomDebug(string levelName)
    {
        if (canLoad)
        {
            Destroy(playerHolder);

            // Unload previous scenes.
            foreach (Scene i in GetLoadedScenes())
            {
                SceneManager.UnloadSceneAsync(i); //using Async because it yells at me otherwise
            }
            SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
            //player = Instantiate(_playerPrefab);
        }

    }


    public void LoadRoom(LevelDataResource levelDataResource)
    {
        Destroy(playerHolder);

        // Unload previous scenes.
        foreach (Scene i in GetLoadedScenes())
        {
            SceneManager.UnloadSceneAsync(i); //using Async because it yells at me otherwise
            //SceneManager.UnloadScene(i);
        }

        // Check what scenes should be loaded based on save data and exit trigger


        // then load them all
        foreach (SceneReference i in GetScenesToLoad(levelDataResource))
        {
            //SceneManager.LoadScene(i.name, LoadSceneMode.Additive);
            SceneManager.LoadScene(i.Name, LoadSceneMode.Additive);
            //Debug.Log(i.name + " was loaded!");
        }
    }
    void ValidateScenes()
    {
        currentLevelData = FindObjectsOfType<LevelData>().ToList<LevelData>(); // TODO: Investigate Object.FindObjectByType instead. BY type, not OF type.
        Validate_No_UNASSIGNED();
        Validate_ExactlyOne_LEVEL();
    }


    List<SceneReference> GetScenesToLoad(LevelDataResource levelDataResource)
    {
        List<SceneReference> scenes = new List<SceneReference>();

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
        //else { Debug.Log("All's well!"); }
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

    public List<LevelDataResource> debugScenes;
    void DebugLoadInput()
    {
        if (canLoad)
        {
            LevelDataResource testResource = ScriptableObject.CreateInstance<LevelDataResource>();

            DebugLoadLevel(0,KeyCode.Alpha1,testResource);
            DebugLoadLevel(1,KeyCode.Alpha2,testResource);
            DebugLoadLevel(2,KeyCode.Alpha3,testResource);
            DebugLoadLevel(3,KeyCode.Alpha4,testResource);
            DebugLoadLevel(4,KeyCode.Alpha5,testResource);
            DebugLoadLevel(5,KeyCode.Alpha6,testResource);
            DebugLoadLevel(6,KeyCode.Alpha7,testResource);
            DebugLoadLevel(7,KeyCode.Alpha8,testResource);
            DebugLoadLevel(8,KeyCode.Alpha9,testResource);
            DebugLoadLevel(9,KeyCode.Alpha0,testResource);
        }
    }
    void DebugLoadLevel(int myIndex, KeyCode keyCode, LevelDataResource testResource)
    {
        if (Input.GetKeyUp(keyCode))
        {
            testResource = debugScenes[myIndex];
            targetSpawnIndex = 0;
            LoadRoom(testResource);
        }
    }
}
