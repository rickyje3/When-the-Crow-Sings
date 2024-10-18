using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerSpawnPoint : MonoBehaviour
{
    // Position and rotation the player should spawn in.

    public int entranceIndex = -1;

    public GameSignal loadingFinishedTEMP;

    private void Start()
    {
        loadingFinishedTEMP.Emit();
    }
}
