using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AssignInputAction : MonoBehaviour
{
    public UnityEvent myEvent;
    public InputActionReference inputAction;

    private void OnEnable()
    {
        inputAction.action.performed += OnPerformed;
    }
    private void OnDisable()
    {
        inputAction.action.performed -= OnPerformed;
    }

    void OnPerformed(InputAction.CallbackContext context)
    {
        myEvent.Invoke();
    }
}
