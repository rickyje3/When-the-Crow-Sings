using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTarget : MonoBehaviour
{
    public Transform player;
    public float radius = 5.0f;
    private void Update()
    {
        Vector3 direction = CameraManager.mouseWorldPosition - player.position;

        if (direction.magnitude > radius)
        {
            direction = direction.normalized * radius;
        }
        
        transform.position = direction + player.position;
    }
}
