using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBrain : StateMachineComponent
{
    public CharacterController controller;
    public CrowHolder crowHolder;

    public float secondsToPeck;

    [HideInInspector]
    public Transform restPoint;

    [HideInInspector]
    public bool targetIsTargetNotSpawn; // Determines whether TargetState should focus on birdseed or spawn.
    [HideInInspector]
    public bool idleWaitingAfterPecking; // Determines whether the idle state should be of infinite length or return to dispersal after a short wait.

    private void Awake()
    {
        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new CrowIdleState(this), "CrowIdleState");
        stateMachine.RegisterState(new CrowScatterState(this), "CrowScatterState");
        stateMachine.RegisterState(new CrowTargetState(this), "CrowTargetState");
        stateMachine.RegisterState(new CrowPeckState(this), "CrowPeckState");
    }
    private void Start()
    {
        stateMachine.Enter("CrowIdleState");
    }

    public void FlyNavigate()
    {
        // if raycast detects surface AND that surface is NOT the destination, then navigate away.
    }

    public void SetTargetAsTarget(bool _target)
    {
        targetIsTargetNotSpawn = _target;
        stateMachine.Enter("CrowTargetState");
    }

    public void OnCrowTargetActivated(SignalArguments args)
    {
        SetTargetAsTarget(true);
    }
    public void OnCrowTargetDeactivated(SignalArguments args)
    {
        stateMachine.Enter("CrowScatterState");
    }

    public void SetRestPoint(Transform _restPoint)
    {
        restPoint = _restPoint;
        transform.position = restPoint.position;
        // TODO: Add rotation.
    }
}