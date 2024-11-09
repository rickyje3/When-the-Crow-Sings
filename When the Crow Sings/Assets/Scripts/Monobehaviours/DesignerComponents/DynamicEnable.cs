using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicEnable : MonoBehaviour
{
    public string associatedDataKey = "";

    public enum VALUE_TYPE { BOOL, INT, STRING }
    public VALUE_TYPE valueType = VALUE_TYPE.BOOL;
    [Header("If BOOL, then fill this.")]
    public bool boolValue;
    [Header("If INT, then fill this.")]
    public int intValue;
    [Header("If STRING, then fill this.")]
    public string stringValue;

    private void Start()
    {
        ServiceLocator.Get<GameManager>().dynamicEnables.Add(this);
    }
    private void OnDestroy()
    {
        ServiceLocator.Get<GameManager>().dynamicEnables.Remove(this);
    }


 

    
}
