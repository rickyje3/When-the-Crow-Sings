using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseCanvas;

    public GameSignal unpauseSignalTEMP;

    private void Start()
    {
        pauseCanvas = GetComponent<Canvas>();
    }

    public void resume()
    {
        unpauseSignalTEMP.Emit();
        pauseCanvas.gameObject.SetActive(false);
        //stateMachine.Enter("PlayerMovementState");
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
