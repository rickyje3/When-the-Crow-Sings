using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public string flagToCheck = "TestingFlag1"; // Only checks boolflags right now.
    [SerializeField]
    private bool flagValueForEnabled;
    private bool _flagValueForEnabled
    {
        get
        {
            return flagValueForEnabled;
        }
        set
        {
            flagValueForEnabled = value;
            gameObject.SetActive(flagValueForEnabled);
        }
    }

    // TODO: Add check in debugmanager to enable/disable visibility. Interface?


}
