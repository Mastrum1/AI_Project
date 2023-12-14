using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReturnToPos : StateMachineBehaviour
{
    public static event Action returnToPos;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        returnToPos?.Invoke();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
    }
}
