using Eflatun.SceneReference;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public SceneReference mainScene;

    public Button sceneLoadButtonPrefab;
    public GridLayoutGroup sceneLoadButtonsHolder;

    public List<LevelDataResource> levelDataResources;

    private void Awake()
    {

        foreach (LevelDataResource i in levelDataResources)
        {
            //i.onClick.AddListener(() => OnSceneLoadButtonPressed(sceneLoadButtonList.IndexOf(i)));
            var x = Instantiate(sceneLoadButtonPrefab);
            x.transform.SetParent(sceneLoadButtonsHolder.transform, false);
            x.onClick.AddListener(() => OnSceneLoadButtonPressed(levelDataResources.IndexOf(i)));
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene.Name);
    }

    public void OnSceneLoadButtonPressed(int index)
    {
        Debug.Log(index);
    }
}
