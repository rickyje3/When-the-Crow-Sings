using ScriptableObjects;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
[System.Serializable]
public class SubSceneContainer
{
    public SceneAsset subScene; //or sceneasset or scene
    public List<SubSceneLogicBase> subSceneLogics = new List<SubSceneLogicBase>();
    public string hi;
}
