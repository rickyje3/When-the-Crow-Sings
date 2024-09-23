using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class StateMachineComponent : MonoBehaviour
{
    protected StateMachine stateMachine;
    private void Update() => stateMachine.Update(Time.deltaTime);
    private void FixedUpdate() => stateMachine.FixedUpdate();
}
