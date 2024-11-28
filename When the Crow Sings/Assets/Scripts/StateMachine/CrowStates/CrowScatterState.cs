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
    
    public override void FixedUpdate()
    {
        s.FlyNavigate();
    }

    public override void StateEntered()
    {
        s.dir = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) *.1f;
    }
}
