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
    public GameObject mainMenuPage;

    private void Awake()
    {
        PopulateSceneLoadDebugButtons();

        mainMenuDebugLoadHolder.resourceToLoad = null; // I don't remember what this does exactly but it's important.

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

    private void PopulateSceneLoadDebugButtons()
    {
        foreach (LevelDataResource i in levelDataResources)
        {
            var x = Instantiate(sceneLoadButtonPrefab);
            x.transform.SetParent(sceneLoadButtonsHolder.transform, false);
            x.onClick.AddListener(() => OnSceneLoadButtonPressed(i));
            x.GetComponentInChildren<TextMeshProUGUI>().text = i.level.Name;
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene.Name);
        mainMenuPage.SetActive(false);
    }

    public void OnSceneLoadButtonPressed(LevelDataResource levelDataResource)
    {
        mainMenuDebugLoadHolder.resourceToLoad = levelDataResource;
        SceneManager.LoadScene(mainScene.Name);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
