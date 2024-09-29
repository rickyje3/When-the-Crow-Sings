using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTarget : MonoBehaviour
{
    public Transform player;
    public float radius = 5.0f;
    private void Update()
    {
        //Vector3 clampedMousePosition = CameraManager.mouseWorldPosition;
        //clampedMousePosition.x = Mathf.Clamp(clampedMousePosition.x, player.position.x - radius, player.position.x + radius);
        //clampedMousePosition.y = Mathf.Clamp(clampedMousePosition.y, player.position.y - radius, player.position.y + radius);
        //clampedMousePosition.z = Mathf.Clamp(clampedMousePosition.z, player.position.z - radius, player.position.z + radius);

        //transform.position = clampedMousePosition;

        Vector3 direction = CameraManager.mouseWorldPosition - player.position;

        if (direction.magnitude > radius)
        {
            direction = direction.normalized * radius;
        }
        
        transform.position = direction + player.position;
    }
}
