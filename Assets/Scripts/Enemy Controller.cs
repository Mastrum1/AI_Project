using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public float detectionTime;
    [SerializeField] float fovAngle;
    [NonSerialized] public float savedTime;

    public static event Action walk;
    public static event Action returnToPos;

    private Animator animator;
    private Vector3 originalPos;

    Ray[] ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("DistanceFromPlayer", Vector3.Magnitude(player.transform.position - transform.position));
        //Debug.Log(transform.forward);

        savedTime += Time.deltaTime;

        CreateFieldOfView();

        CheckRayHit();

        CheckStartPos();

        if (animator.GetFloat("DistanceFromPlayer") < 2)
        {
            animator.SetBool("IsInRange", true);
        }
        else
        {
            animator.SetBool("IsInRange", false);
        }
    }

    private void WalkBack()
    {
        agent.SetDestination(originalPos);
    }

    private void CheckRayHit()
    {
        for (int i = 0; i < ray.Length - 1; i++)
        {
            if (Physics.Raycast(ray[i], out hit, 20))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    //Debug.Log(savedTime);
                    savedTime = 0;

                    animator.SetBool("IsDetected", true);

                    walk?.Invoke();
                }
            }
            else
            {
                animator.SetBool("IsDetected", false);
            }
        }
    }

    private void CreateFieldOfView()
    {
        ray = new Ray[2];

        ray[0] = new Ray(transform.position, transform.forward);

        for (int i = 1; i < ray.Length; i++)
        {
            float rand = UnityEngine.Random.Range(-fovAngle, fovAngle);

            ray[i] = new Ray(Quaternion.AngleAxis(rand, transform.up) * transform.forward, transform.forward);

            Debug.DrawRay(transform.position, Quaternion.AngleAxis(rand, transform.up) * transform.forward, Color.red);
        }
    }

    private void CheckStartPos()
    {
        if (originalPos != transform.position)
        {
            animator.SetBool("IsNotAtStartingPos", true);

            ReturnToPos.returnToPos += WalkBack;
        }
        else
        {
            animator.SetBool("IsNotAtStartingPos", false);

            ReturnToPos.returnToPos -= WalkBack;
        }
    }
}
