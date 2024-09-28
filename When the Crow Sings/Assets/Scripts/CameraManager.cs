using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public static Vector3 mouseWorldPosition;

 

    void Update()
    {

        Ray ray = GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            
            Debug.DrawLine(ray.origin, hitInfo.point,Color.red,1f);
            mouseWorldPosition = hitInfo.point;
            
        }
        else { mouseWorldPosition = Vector3.zero; }
    }
}
