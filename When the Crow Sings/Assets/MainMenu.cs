using Eflatun.SceneReference;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public MainMenuDebugLoadHolder mainMenuDebugLoadHolder;

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
            x.onClick.AddListener(() => OnSceneLoadButtonPressed(i));
            x.GetComponentInChildren<TextMeshProUGUI>().text = i.name;
        }

        mainMenuDebugLoadHolder.resourceToLoad = null;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene.Name);
    }

    public void OnSceneLoadButtonPressed(LevelDataResource levelDataResource)
    {
        //Debug.Log(index);
        mainMenuDebugLoadHolder.resourceToLoad = levelDataResource;
        SceneManager.LoadScene(mainScene.Name);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
