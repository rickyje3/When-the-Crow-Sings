using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public InputManager inputManager;
    public static bool isPaused
    {
        get
        {
            return Time.timeScale == 0f;
        }
    }

    public void OnPaused(SignalArguments args)
    {
        inputManager.EnablePlayerInput(false);
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        inputManager.EnablePlayerInput(true);
    }

    public void Journal()
    {
        //journal activate here
    }

    public void QuitToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
