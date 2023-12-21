using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspecting : StateMachineBehaviour
{
    float time;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Quaternion myRot = Quaternion.identity;
        myRot.y += 5;
        animator.gameObject.transform.rotation = myRot;

        time += Time.deltaTime;

        if (time >= 5)
        {
            time = 0;
            animator.SetBool("IsInspecting", false);
        }
    }
}
