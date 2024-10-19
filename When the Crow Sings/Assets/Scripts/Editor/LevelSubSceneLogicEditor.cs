using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements;


[CustomEditor(typeof(LevelData))]
public class LevelSubSceneLogicEditor : Editor
{
    public VisualTreeAsset visualTree;


    private PropertyField toggleVisibility;


    VisualElement visualElementsToHide;

    private void OnBoolChanged(ChangeEvent<bool> myEvent)
    {
        
    }


    public override VisualElement CreateInspectorGUI()
    {
        //return base.CreateInspectorGUI();
        VisualElement root = new VisualElement();
        
        // Add in the UI builder stuff
        visualTree.CloneTree(root);

        toggleVisibility = root.Q<PropertyField>("toggleBool");
        toggleVisibility.RegisterCallback<ChangeEvent<bool>>(OnBoolChanged);


        return root;
    }
}
