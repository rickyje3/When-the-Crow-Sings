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
        if (InputManager.inputDevice == InputManager.InputDevices.MOUSE_AND_KEYBOARD)
        {
            direction = CameraManager.mouseWorldPosition - player.position;
            hasReceivedInput = true;
        }
            
        else
        {
            var oldDirection = direction;
            direction += new Vector3(controllerInput.x * controllerSpeedOffset, 0, controllerInput.y * controllerSpeedOffset);

            if (oldDirection - direction != Vector3.zero) hasReceivedInput = true;

        }
        
        //if (direction.magnitude > 0) hasReceivedInput = true;

        if (direction.magnitude > radius)
        {
            direction = direction.normalized * radius;
        }

        if (hasReceivedInput)
            transform.position = direction + player.position;
    }

    bool hasReceivedInput = false;
    private void OnEnable()
    {
        direction = Vector3.zero;
        hasReceivedInput = false;

        if (InputManager.inputDevice != InputManager.InputDevices.MOUSE_AND_KEYBOARD)
        {
            direction = Quaternion.Euler(0, player.rotation.eulerAngles.y, 0) * new Vector3(0, 0, 5);//(Quaternion.Euler(0,transform.rotation.eulerAngles.y,0)*(new Vector3(0, 0, 1)) );
            transform.position = direction + player.position;
        }
            
    }
}
