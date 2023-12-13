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

    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("DistanceFromPlayer", Vector3.Magnitude(player.transform.position - transform.position));

        ray = new Ray(transform.position, transform.forward);
       
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        CheckRayHit();

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
        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.up);
    }

    private void CheckRayHit()
    {
        if (Physics.Raycast(ray, out hit, 20))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                Debug.Log(hit.transform.gameObject.tag);
                animator.SetBool("IsDetected", true);

                Walk.walk += ChasePlayer;
            }
        }
        else
        {
            animator.SetBool("IsDetected", false);

            Walk.walk -= ChasePlayer;
        }
    }
}
