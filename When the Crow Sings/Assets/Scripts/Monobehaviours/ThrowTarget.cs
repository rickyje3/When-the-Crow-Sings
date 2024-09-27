using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTarget : MonoBehaviour
{
    private void Update()
    {
        transform.position = CameraManager.mouseWorldPosition;
    }
}
