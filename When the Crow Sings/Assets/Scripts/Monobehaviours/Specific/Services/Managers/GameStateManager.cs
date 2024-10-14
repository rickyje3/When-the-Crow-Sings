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


    void LoadRoom()
    {
        // unload previous scenes

        // first check what scenes should be loaded based on save data
        
        // then load them all

        // get all of the spawners, determine which one to use based on which room was left
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(1);//, LoadSceneMode.Additive);
        }
    }


}
