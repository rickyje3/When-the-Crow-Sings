using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SignalArguments
{
    public List<string> stringArgs = new List<string>();
    public List<int> intArgs = new List<int>();
    public List<float> floatArgs = new List<float>();
    public List<bool> boolArgs = new List<bool>();
    public List<Object> objectArgs = new List<Object>();
}
