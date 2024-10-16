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

    void LoadRoom()
    {
        currentLevelData = FindObjectsOfType<LevelData>().ToList<LevelData>();
        
        ValidateNoUNASSIGNED();
        ValidateOnlyOneLEVEL();





        // unload previous scenes

            // first check what scenes should be loaded based on save data

            // then load them all

            // get all of the spawners, determine which one to use based on which room was left
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(1);//, LoadSceneMode.Additive);
        }
    }


}
