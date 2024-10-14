using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour, IService
{
    private void Awake()
    {
        RegisterSelfAsService();
    }
    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<GameStateManager>(this);
    }



    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(1);//, LoadSceneMode.Additive);
        }
    }


}
