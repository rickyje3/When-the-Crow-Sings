using System.Collections.Generic;
using UnityEditor;
[System.Serializable]
public class SubSceneContainer
{
    public SceneAsset subScene; //or sceneasset or scene
    public List<SubSceneLogicBase> subSceneLogics = new List<SubSceneLogicBase>();
}
