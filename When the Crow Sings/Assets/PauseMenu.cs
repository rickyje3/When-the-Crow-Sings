using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : StateMachineComponent
{
    public Canvas pauseCanvas;

    private void Start()
    {
        pauseCanvas = GetComponent<Canvas>();
    }

    public void resume()
    {
        pauseCanvas.enabled = false;
        stateMachine.Enter("PlayerMovementState");
    }

    public void journal()
    {
        //journal activate here
    }

    public void quitToMain()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
