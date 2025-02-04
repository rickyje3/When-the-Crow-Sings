using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowRestPoint : MonoBehaviour
{
    public GameObject debugVisible;
    private void OnEnable()
    {
        ServiceLocator.Get<GameManager>().RegisterCrowRestPoint(this);
    }
    private void OnDisable()
    {
        ServiceLocator.Get<GameManager>().UnregisterCrowRestPoint(this);
    }
    private void Awake()
    {
        debugVisible.SetActive(false);
    }
}
