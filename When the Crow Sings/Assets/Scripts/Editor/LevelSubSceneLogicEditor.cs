using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements;


[CustomEditor(typeof(LevelData))]
public class LevelSubSceneLogicEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        //return base.CreateInspectorGUI();
        VisualElement root = new VisualElement();
        Debug.Log("Anything??");
        return root;
    }
}
