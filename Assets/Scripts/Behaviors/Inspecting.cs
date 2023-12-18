using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspecting : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Quaternion myRot = Quaternion.identity;
        myRot.x += 5;
        animator.gameObject.transform.rotation = myRot;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
    }
}
