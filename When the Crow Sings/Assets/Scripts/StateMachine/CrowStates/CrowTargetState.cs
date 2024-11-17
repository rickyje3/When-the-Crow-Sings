using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowTargetState : StateMachineState
{
    BirdBrain s;
    public CrowTargetState(BirdBrain birdBrain)
    {
        s = birdBrain;
    }

    Vector3 destination;
    Vector3 dir;
    public override void FixedUpdate()
    {
        dir = (destination - s.transform.position)*.01f;
        s.controller.Move(dir);//*Time.deltaTime);
    }
    public override void StateEntered()
    {
        destination = s.crowHolder.CrowTarget.transform.position;
    }
}
