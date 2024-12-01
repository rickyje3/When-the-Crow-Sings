using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVisualEnabler : MonoBehaviour
{
    void Update()
    {
        if (DebugManager.showCollidersAndTriggers == false) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
}