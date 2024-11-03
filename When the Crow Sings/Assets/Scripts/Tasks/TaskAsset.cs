using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tasks/TaskAsset")]
public class TaskAsset : ScriptableObject
{
    //public string taskName;
    //public List<TaskStepBase> taskSteps;
    public TaskStepBase steps;
}
