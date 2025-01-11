using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
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

        InputManager.playerInputActions.UI.Enable();
        InputManager.playerInputActions.UI.Unpause.performed += OnPauseButtonPressed;
        
    }

    public void Resume()
    {
        Debug.Log("Resuming");


        InputManager.playerInputActions.UI.Unpause.performed -= OnPauseButtonPressed;
        InputManager.playerInputActions.UI.Disable();

        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        inputManager.EnablePlayerInput(true);
    }

    public void Journal()
    {
        //journal activate here
    }

    private void OnPauseButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Unpause pressed");
        Resume();
    }

    public void QuitToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu_SCN");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        InputManager.playerInputActions.UI.Unpause.performed -= OnPauseButtonPressed;
    }
}
