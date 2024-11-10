using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowPeckState : StateMachineState
{
    BirdBrain s;
    public CrowPeckState(BirdBrain birdBrain)
    {
        s = birdBrain;
    }
}
