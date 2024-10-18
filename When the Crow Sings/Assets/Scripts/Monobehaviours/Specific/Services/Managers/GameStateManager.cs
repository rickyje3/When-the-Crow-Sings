using ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour, IService
{
    public GameSignal levelLoadStartSignal;
    public GameSignal levelLoadFinishSignal;

    List<LevelData> currentLevelData = new List<LevelData>();

    const string SCN_PATH = "Assets/Scenes/";

    // ---------------------------------------------------------------------------
    private void Awake()
    {
        RegisterSelfAsService();
    }
    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<GameStateManager>(this);
    }

    //private void Start()
    //{
    //    GetLoadedScenes();
    //}
    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        foreach (Scene i in GetLoadedScenes())
    //        {
    //            SceneManager.UnloadSceneAsync(i); //using Async because it yells at me otherwise
    //        }
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha1))
    //    {
    //        LoadRoom(1);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha2))
    //    {
    //        LoadRoom(2);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha3))
    //    {
    //        LoadRoom(3);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha4))
    //    {
    //        LoadRoom(4);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha5))
    //    {
    //        LoadRoom(5);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha6))
    //    {
    //        LoadRoom(6);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha7))
    //    {
    //        LoadRoom(7);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha8))
    //    {
    //        LoadRoom(8);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha9))
    //    {
    //        LoadRoom(9);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha0))
    //    {
    //        LoadRoom(10);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Minus))
    //    {
    //        LoadRoom(11);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Z))
    //    {
    //        LoadRoom(12);
    //    }
    //    if (Input.GetKeyUp(KeyCode.X))
    //    {
    //        LoadRoom(13);
    //    }
    //}


    // ---------------------------------------------------------------------------

    void LoadRoom(int whichTEMP)
    {
        // Unload previosu scenes.
        foreach (Scene i in GetLoadedScenes())
        {
            SceneManager.UnloadSceneAsync(i); //using Async because it yells at me otherwise
        }

        // Check what scenes should be loaded based on save data and exit trigger

        // then load them all
        foreach (Scene i in GetScenesToLoad(whichTEMP))
        {
            //SceneManager.LoadScene(i.name, LoadSceneMode.Additive);
            SceneManager.LoadScene(whichTEMP, LoadSceneMode.Additive);
        }

        StartCoroutine(NextFrameSteps());
    }

    public void OnLoadStart(SignalArguments args)
    {

    }
    public void OnLoadFinish(SignalArguments args)
    {

    }


    IEnumerator NextFrameSteps()
    {
        yield return null;
        ValidateScenes();
        // get all of the spawners, determine which one to use based on which room was left
        FindObjectOfType<PlayerController>().transform.position = FindObjectOfType<PlayerSpawnPoint>().transform.position;
        FindObjectOfType<PlayerController>().movementInput = Vector3.zero;
    }
    void ValidateScenes()
    {
        currentLevelData = FindObjectsOfType<LevelData>().ToList<LevelData>(); // TODO: Investigate Object.FindObjectByType instead. BY type, not OF type.
        ValidateNoUNASSIGNED();
        ValidateOnlyOneLEVEL();
    }

    List<Scene> GetScenesToLoad(int whichTEMP)
    {
        List<Scene> scenes = new List<Scene>();
        scenes.Add(SceneManager.GetSceneByBuildIndex(whichTEMP+1));

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



    void ValidateOnlyOneLEVEL()
    {
        if (currentLevelData.Count(x => x.sceneType == LevelData.SceneType.LEVEL) > 1)
            throw new System.Exception("More than one LEVEL-type scene loaded!");

    }
    void ValidateNoUNASSIGNED()
    {
        foreach (LevelData i in currentLevelData)
        {
            if (i.sceneType == LevelData.SceneType.UNASSIGNED) throw new System.Exception("Attempting to load a level of type UNASSIGNED!");
        }
    }




    


}
