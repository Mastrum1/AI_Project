using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.GetChild(3).gameObject.SetActive(false);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.GetChild(3).gameObject.SetActive(true);
    }
}
