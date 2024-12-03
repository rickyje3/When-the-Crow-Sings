using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTarget : MonoBehaviour
{
    public Transform player;
    public float radius = 5.0f;

    [HideInInspector]
    public Vector2 controllerInput = new Vector2();
    Vector3 direction = new Vector3();

    public float controllerSpeedOffset = 0.1f;
    private void Update()
    {
        if (InputManager.inputDevice == InputManager.InputDevices.MOUSE_AND_KEYBOARD) direction = CameraManager.mouseWorldPosition - player.position;
        else direction += new Vector3(controllerInput.x* controllerSpeedOffset, 0,controllerInput.y* controllerSpeedOffset);

        if (direction.magnitude > radius)
        {
            direction = direction.normalized * radius;
        }
        
        transform.position = direction + player.position;
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }
}
