using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public bool GoingToHub;
    public bool GoingToZone1;
    public bool GoingToZone2;
    public bool GoingToZone3;
    public bool GoingToZone4;
    public bool GoingToStealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GoingToHub)
            {
                SceneManager.LoadScene("Hub");
            }
            if (GoingToZone4)
            {
                SceneManager.LoadScene("Zone4Blockout");
            }
            if (GoingToStealth)
            {
                SceneManager.LoadScene("enemy_test_scene");
            }
        }
    }
}
