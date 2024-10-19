using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

[System.Serializable]
public class SubSceneLogicBase
{
    public string associatedData = "";
    public enum VALUE_TYPE { BOOL, INT }
    public VALUE_TYPE valueType = VALUE_TYPE.BOOL;
    public bool boolValue;
}
