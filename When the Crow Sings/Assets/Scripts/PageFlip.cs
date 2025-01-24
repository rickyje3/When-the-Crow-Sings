using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFlip : MonoBehaviour
{
    public bool isRight;
    float objectHeight;
    float midPage;
    Vector3 rotationCenter;

    private void Start()
    {
        objectHeight = this.transform.lossyScale.y;
        midPage = objectHeight / 2;
        Vector3 rotationCenter = transform.position = new Vector3(0, objectHeight, midPage);
    }

    public void flipLeft()
    {
        if(isRight)
        {
            transform.RotateAround(rotationCenter, Vector3.left, 90f);
        }
    }

    public void flipRight()
    {
        if (!isRight)
        {
            transform.RotateAround(rotationCenter, Vector3.right, 90f);
        }
    }
}
