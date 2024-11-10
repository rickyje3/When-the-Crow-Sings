using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowScatterState : StateMachineState
{
    BirdBrain s;
    public CrowScatterState(BirdBrain birdBrain)
    {
        s = birdBrain;
    }



    Vector3 dir;
    public override void FixedUpdate()
    {
        s.controller.Move(dir);//*Time.deltaTime);
    }

    public override void StateEntered()
    {
        Debug.Log("SCATTER");
        dir = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) *.1f;
    }
}
