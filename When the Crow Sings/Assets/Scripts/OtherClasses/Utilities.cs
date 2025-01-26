using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Utilities
{
    public static float GetAngleFromVector_Deg(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    //public static float GetAngleFromVector_Rad(Vector3 dir)
    //{
    //    dir = dir.normalized;
    //    float n = Mathf.Atan2(dir.y, dir.x);
    //    if (n < 0) n += 360;
    //    return n;
    //}

    public static string RemoveFirstOccurence(string removeThis, string fromThis)
    {
        int index = fromThis.IndexOf(removeThis);
        string newString = (index < 0)
            ? fromThis
            : fromThis.Remove(index, removeThis.Length);
        return newString;
    }

    public static int GetSingleIntFromString(string str)
    {
        Match result = Regex.Match(str, @"-?\d+");

        if (result.Success)
        {
            return int.Parse(result.Value);
        }
        return 0;
    }


}
