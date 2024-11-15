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

    public GameObject newGameButtons;
    public GameObject continueGameButtons;

    private void Awake()
    {

        foreach (LevelDataResource i in levelDataResources)
        {
            //i.onClick.AddListener(() => OnSceneLoadButtonPressed(sceneLoadButtonList.IndexOf(i)));
            var x = Instantiate(sceneLoadButtonPrefab);
            x.transform.SetParent(sceneLoadButtonsHolder.transform, false);
            x.onClick.AddListener(() => OnSceneLoadButtonPressed(i));
            x.GetComponentInChildren<TextMeshProUGUI>().text = i.level.Name;
        }

        mainMenuDebugLoadHolder.resourceToLoad = null;

        if (SaveData.SavedDataExists())
        {
            newGameButtons.SetActive(false);
            continueGameButtons.SetActive(true);
        }
        else
        {
            newGameButtons.SetActive(true);
            continueGameButtons.SetActive(false);
        }
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
