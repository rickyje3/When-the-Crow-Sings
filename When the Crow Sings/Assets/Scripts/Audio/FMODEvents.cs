using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }


    [field: Header("QTE Success Sound")]
    [field: SerializeField] public EventReference QteSucceeded { get; private set; }
    [field: Header("QTE Fail Sound")]
    [field: SerializeField] public EventReference QteFailed { get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Event scripts in the scene");

        }
        instance = this;
    }
}
