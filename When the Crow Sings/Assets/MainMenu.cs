using Eflatun.SceneReference;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public SceneReference mainScene;

    public List<Button> sceneLoadButtonList;

    private void Awake()
    {

        foreach (Button i in sceneLoadButtonList)
        {
            i.onClick.AddListener(() => OnSceneLoadButtonPressed(sceneLoadButtonList.IndexOf(i)));
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
