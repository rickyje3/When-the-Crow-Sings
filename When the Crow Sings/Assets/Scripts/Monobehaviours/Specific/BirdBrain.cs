using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBrain : StateMachineComponent
{
    public CharacterController controller;
    public CrowHolder crowHolder;

    public float secondsToPeck;

    public Animator crowAnimator;

    [HideInInspector]
    public CrowRestPoint restPoint;

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

    [HideInInspector]
    public Vector3 destination, dir;
    public void FlyNavigate()
    {
        // if raycast detects surface AND that surface is NOT the destination, then navigate away.
        controller.Move(dir);//*Time.deltaTime);
    }

    public void SetTargetAsTarget(bool _target)
    {
        targetIsTargetNotSpawn = _target;
        stateMachine.Enter("CrowScatterState");
    }

    public void OnCrowTargetActivated(SignalArguments args)
    {
        SetTargetAsTarget(true);
    }
    public void OnCrowTargetDeactivated(SignalArguments args)
    {
        SetTargetAsTarget(false);
    }

    public void SetRestPoint(CrowRestPoint _restPoint)
    {
        restPoint = _restPoint;
        //transform.position = restPoint.position;
        // TODO: Add rotation.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CrowRestPoint>() == restPoint && !targetIsTargetNotSpawn) stateMachine.Enter("CrowIdleState");
        if (other.GetComponent<CrowTarget>() && targetIsTargetNotSpawn) stateMachine.Enter("CrowPeckState");
    }
}