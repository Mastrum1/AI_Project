using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent agent;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("DistanceFromPlayer", Vector3.Magnitude(player.transform.position - transform.position));
        //transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.up);

        Walk.walk += ChasePlayer;

        if (animator.GetFloat("DistanceFromPlayer") < 20)
        {
            animator.SetBool("IsDetected", true);
        }
        else
        {
            animator.SetBool("IsDetected", false);
        }

        if (animator.GetFloat("DistanceFromPlayer") < 2)
        {
            animator.SetBool("IsInRange", true);
        }
        else
        {
            animator.SetBool("IsInRange", false);
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }
}
