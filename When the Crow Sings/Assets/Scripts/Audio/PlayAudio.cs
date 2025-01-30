using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class PlayAudio : MonoBehaviour
{
    [field: SerializeField] public EventReference Sound { get; private set; }
    public bool playOnAwake;

    private void Start()
    {
        if (playOnAwake)
        {
            PlayOneShot();
        }
    }


    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Sound);  
    }
}
