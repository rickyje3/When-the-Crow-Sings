using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class PlayAudio : MonoBehaviour
{
    [field: SerializeField] public EventReference Sound { get; private set; }
    public bool playOnAwake;

    private GameObject subject;
    public bool SubjectHasPlayed = true; //dont pay attention to the wording this wont make sense
    private bool hasCheckedInitialState = true;

    private void Start()
    {
        if (playOnAwake)
        {
            PlayOneShot();
        }

        /*SubjectHasPlayed = true;

        if (transform.childCount > 0)
        {
            subject = transform.GetChild(0).gameObject; // Get the first child
            if (subject.activeSelf == false)
            {
                Destroy(subject);
                if (this != null)
                {
                    Debug.Log("Destroyed because the subjects child is inactive");
                    Destroy(this);
                }
            }
            else if(subject.activeSelf) SubjectHasPlayed = false;
        }
        else
        {
            Debug.Log("This object has no children!");
        }*/
    }

    /*private void Update()
    {
        //Skip the first frame check
        if (!hasCheckedInitialState)
        {
            hasCheckedInitialState = true;
            return;
        }

        if (subject != null && subject.activeSelf == false && !SubjectHasPlayed)
        {
            PlayOneShotOnce();
        }
    }*/

    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Sound);  
    }

    /*public void PlayOneShotOnce()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Sound);
        Debug.Log("Subject playing");
        SubjectHasPlayed = true;
        //Destroy(subject);
        //Destroy(this.gameObject);
    }*/
}
