using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    // TODO: Not here, but definitely find a way to more easily reference scenes than their build index/string.
    // https://www.reddit.com/r/Unity3D/comments/1888oax/i_beg_you_dont_use_the_buildindex_for_loading/ has some insights.

    public enum SceneType { UNASSIGNED, LEVEL, MAIN, VARIABLE }

    public SceneType sceneType = SceneType.UNASSIGNED;

    //[Header("UNASSIGNED")]

    [Header("LEVEL")]
    [HideInInspector]
    public List<PlayerSpawnPoint> spawnPoints;

    [Header("MAIN")]
    [HideInInspector]
    public int ThisVariableDoesNothingItsJustHereToHaveSomethingForNow;


    public List<SubSceneContainer> subScenes;



    public GameSignal loadingFinished;


    private void Awake()
    {
        if (sceneType != SceneType.LEVEL && subScenes != null)
        {
            throw new System.Exception("A non-LEVEL scene has SubScenes listed!");
        }
    }
    private void Start()
    {
        if (sceneType == SceneType.LEVEL)
        {
            loadingFinished.Emit();
        }
        
    }
}
