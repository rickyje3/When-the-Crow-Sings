using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class StateMachineComponent : MonoBehaviour
{
    public StateMachine stateMachine;
    private void Update() => stateMachine.Update(Time.deltaTime);
    private void FixedUpdate() => stateMachine.FixedUpdate();

    private void OnTriggerEnter(Collider other)
    {
        stateMachine.OnTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        stateMachine.OnTriggerExit(other);
    }
}
