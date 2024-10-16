using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour, IService
{
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

    private void Start()
    {
        GetLoadedScenes();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            foreach (Scene i in GetLoadedScenes())
            {
                SceneManager.UnloadSceneAsync(i); //using Async because it yells at me otherwise
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            LoadRoom();
        }
    }


    // ---------------------------------------------------------------------------

    void LoadRoom()
    {
        // Unload previosu scenes.
        foreach (Scene i in GetLoadedScenes())
        {
            SceneManager.UnloadSceneAsync(i); //using Async because it yells at me otherwise
        }

        // Check what scenes should be loaded based on save data and exit trigger

        // then load them all
        foreach (Scene i in GetScenesToLoad())
        {
            //SceneManager.LoadScene(i.name, LoadSceneMode.Additive);
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }

        StartCoroutine(ValidateScenesOnNextFrame());

        // get all of the spawners, determine which one to use based on which room was left
        FindObjectOfType<PlayerController>().transform.position = FindObjectOfType<PlayerSpawnPoint>().transform.position;
    }


    IEnumerator ValidateScenesOnNextFrame()
    {
        yield return null;
        ValidateScenes();
    }
    void ValidateScenes()
    {
        currentLevelData = FindObjectsOfType<LevelData>().ToList<LevelData>(); // TODO: Investigate Object.FindObjectByType instead. BY type, not OF type.
        ValidateNoUNASSIGNED();
        ValidateOnlyOneLEVEL();
    }

    List<Scene> GetScenesToLoad()
    {
        List<Scene> scenes = new List<Scene>();
        scenes.Add(SceneManager.GetSceneByBuildIndex(3));

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
