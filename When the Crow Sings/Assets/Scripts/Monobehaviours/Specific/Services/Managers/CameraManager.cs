using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public static Vector3 mouseWorldPosition;

    void Update()
    {
        SetMouseWorldPosition();
    }

    private void SetMouseWorldPosition()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask("Floor")))//~LayerMask.GetMask("Player","Enemy")))
        {
            mouseWorldPosition = hitInfo.point;
        }
    }
}
