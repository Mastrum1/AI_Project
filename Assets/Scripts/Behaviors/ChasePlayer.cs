using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using Unity.VisualScripting;

public class ChasePlayer : StateMachineBehaviour
{
    private GameObject player;
    private GameObject user;
    private NavMeshAgent agent;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        user = animator.gameObject;
        agent = user.GetComponent<EnemyController>().agent;
        player = user.GetComponent<EnemyController>().player;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (user.GetComponent<EnemyController>().savedTime <= user.GetComponent<EnemyController>().detectionTime)
        {
            agent.SetDestination(player.transform.position);

            user.transform.rotation = Quaternion.LookRotation(player.transform.position - user.transform.position, user.transform.up);
        }
        else return;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
    }
}
