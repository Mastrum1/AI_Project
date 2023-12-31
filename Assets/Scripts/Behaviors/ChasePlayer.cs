using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using Unity.VisualScripting;

public class ChasePlayer : StateMachineBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private EnemyController user;
    private float savedTime;
    private float detectionTime;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        user = animator.gameObject.GetComponent<EnemyController>();
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        player = user.GetComponent<EnemyController>().player;
        detectionTime = user.detectionTime;
        savedTime = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        savedTime += Time.deltaTime;

        if (savedTime <= detectionTime)
        {
            if (user.gameObject.tag == "Berserker" && (user._currentHp/user.maxHp * 100) <= 50)
            {
                agent.speed += 5;
            }
            agent.SetDestination(player.transform.position);

            user.transform.rotation = Quaternion.LookRotation(player.transform.position - user.transform.position, user.transform.up);
        }
        else animator.SetBool("IsDetected", false);
    }
}
