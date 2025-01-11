using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;

    public static AudioManager instance { get; private set; }
    public AreaMusic areaMusic { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one audio manager in the scene");
        }
        instance = this;

        eventInstances = new List<EventInstance>();

        areaMusic = FindObjectOfType<AreaMusic>();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void PlayOneShot(EventReference sound) // hopefully this overload removes the spatialness?
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public bool IsSoundPlaying(EventReference sound)
    {
        return false;
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        if (eventReference.IsNull)
        {
            Debug.LogWarning("Attempted to create an EventInstance with a null EventReference.");
            return default;
        }

        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
