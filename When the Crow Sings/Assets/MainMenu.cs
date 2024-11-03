using Eflatun.SceneReference;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public SceneReference mainScene;

    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene.Name);
    }
}
