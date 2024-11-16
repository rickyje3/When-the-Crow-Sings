using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBrain : StateMachineComponent
{
    public CharacterController controller;
    public CrowHolder crowHolder;

    public float secondsToPeck;


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

    public void OnCrowTargetActivated(SignalArguments args)
    {
        stateMachine.Enter("CrowTargetState");
    }
    public void OnCrowTargetDeactivated(SignalArguments args)
    {
        stateMachine.Enter("CrowScatterState");
    }
}