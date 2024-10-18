using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using static UnityEngine.ParticleSystem;

[InitializeOnLoad]
public class EditorInitializer
{
    // your first scene path:
    const string firstSceneToLoad = "Assets/Scenes/MainScene.unity";
    // Editor pref save name, no need to change
    const string activeEditorScene = "PreviousScenePath";
    const string isEditorInitialization = "EditorIntialization";


    static bool useFancy = true;
    static EditorInitializer()
    {
        if (useFancy)
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        else
        {
            EditorApplication.playModeStateChanged += BackToBasics;
        }
        
    }

    static void BackToBasics(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            EditorPrefs.SetString(activeEditorScene, sceneName);
            EditorPrefs.SetBool(isEditorInitialization, true);
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorPrefs.GetString(activeEditorScene));

        }
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            EditorPrefs.SetBool(isEditorInitialization, false);
        }
    }
    static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        Debug.Log("EditorInitializer is happening!");

        // remove "IsValidScene" method if you want to do the initialization in all scenes.
        if (state == PlayModeStateChange.ExitingEditMode)// && IsValidScene(validScenes, out string sceneName))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            EditorPrefs.SetString(activeEditorScene, sceneName);
            EditorPrefs.SetBool(isEditorInitialization, true);
            SetStartScene(firstSceneToLoad);
        }
        if (state == PlayModeStateChange.EnteredPlayMode && EditorPrefs.GetBool(isEditorInitialization))
        {
            LoadExtraScenes();
        }
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            EditorPrefs.SetBool(isEditorInitialization, false);
        }
        
    }
    static void SetStartScene(string scenePath)
    {
        SceneAsset firstSceneToLoad = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        if (firstSceneToLoad != null)
            EditorSceneManager.playModeStartScene = firstSceneToLoad;
        else
            Debug.Log("Could not find Scene " + scenePath);
    }
    static void LoadExtraScenes()
    {
        string prevScene = EditorPrefs.GetString(activeEditorScene);
        Debug.Log("PrevScene is" + prevScene);
        if (prevScene != "MainScene")
        {
            SceneManager.LoadScene(prevScene, LoadSceneMode.Additive);
        }
    }
}