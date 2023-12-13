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
    [SerializeField] float detectionTime;

    private Animator animator;
    private float savedTime;

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

        savedTime += Time.deltaTime;

        CreateFieldOfView();

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
        //Debug.Log(savedTime);
        if (savedTime <= detectionTime)
        {
            agent.SetDestination(player.transform.position);
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.up);
        }
        else return;
    }

    private void CheckRayHit()
    {
        if (Physics.Raycast(ray, out hit, 20))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                //Debug.Log(savedTime);
                savedTime = 0;

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

    private void CreateFieldOfView()
    {
        ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }
}
