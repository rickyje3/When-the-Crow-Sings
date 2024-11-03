using Eflatun.SceneReference;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class LevelDataResource : ScriptableObject
{
    public SceneReference level;
    public List<SubSceneContainer> subScenes = new List<SubSceneContainer>();
}
