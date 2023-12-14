using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class ReturnToPos : StateMachineBehaviour
{
    NavMeshAgent agent;
    GameObject user;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        user = animator.gameObject;
        agent = user.GetComponent<EnemyController>().agent;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(user.GetComponent<EnemyController>().originalPos);

        user.transform.rotation = Quaternion.LookRotation(user.GetComponent<EnemyController>().originalPos - user.transform.position, user.transform.up);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
    }
}
