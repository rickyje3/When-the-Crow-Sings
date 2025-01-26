using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSignals : StateMachineBehaviour
{
    public string stateName;
    public GameSignal animationStarted;
    public GameSignal animationFinished;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SignalArguments args = new SignalArguments();
        args.stringArgs.Add(stateName);
        animationFinished.Emit(args);
    }
}
