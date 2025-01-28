using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class PlayFootsteps : MonoBehaviour
{
    [field: SerializeField] public EventReference ConcreteFootsteps { get; private set; }
    [field: SerializeField] public EventReference GrassFootsteps { get; private set; }

    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ConcreteFootsteps);  
    }
}
