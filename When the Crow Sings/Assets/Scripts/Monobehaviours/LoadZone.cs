using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadZone : MonoBehaviour
{
    
    public int targetSpawnPointIndex = 0;
    public LevelDataResource levelDataResource;

    [Header("Built-In")]
    public GameSignal startLoadSignal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInteractionArea>() != null)
        {
            Debug.Log("Going to try to load!");
            SignalArguments args = new SignalArguments();
            args.intArgs.Add(targetSpawnPointIndex);
            args.objectArgs.Add(levelDataResource);

            this.gameObject.SetActive(false);

            startLoadSignal.Emit(args);
        }
    }
}
