using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary
{
    [SerializeField]
    public List<SerializableDictionaryElement> elements = new List<SerializableDictionaryElement>();

    public void ValidateNotBlank()
    {
        foreach (SerializableDictionaryElement i in elements)
        {
            if (i.key == null || i.key == "")
            {
                throw new Exception("Key is blank in serializable dictionary!");
            }
        }
    }
}

[Serializable]
public class SerializableDictionaryElement
{
    public string key;
    public bool value;
}