using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary
{
    [SerializeField]
    public List<SerializableDictionaryElement> elements = new List<SerializableDictionaryElement>();
}

[Serializable]
public class SerializableDictionaryElement
{
    public string key;
    public bool value;
}