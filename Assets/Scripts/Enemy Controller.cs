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
    [SerializeField] float HP;
    [NonSerialized] public float savedTime;
    [NonSerialized] public Vector3 originalPos;
    [SerializeField] public Animator minion;

    private Animator animator;
    private float currentHP;
    private bool called;    

    Ray[] ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        originalPos = transform.position;
        currentHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();

        CreateFieldOfView();

        CheckRayHit();

        CheckStartPos();

        CheckIfInRange();

        CheckIfRetreating();
        
        if (!called)
        {
            StartCoroutine(CheckIfInvisible());
        }
    }

    private void CreateFieldOfView()
    {
        ray = new Ray[2];

        ray[0] = new Ray(transform.position, transform.forward);

        for (int i = 1; i < ray.Length; i++)
        {
            float rand = UnityEngine.Random.Range(-fovAngle, fovAngle);

            ray[i] = new Ray(Quaternion.AngleAxis((rand / 2), transform.up) * transform.forward, transform.forward);

            Debug.DrawRay(transform.position, Quaternion.AngleAxis((rand / 2), transform.up) * transform.forward, Color.red);
        }
    }

    private void CheckRayHit()
    {
        savedTime += Time.deltaTime;

        for (int i = 0; i < ray.Length - 1; i++)
        {
            

            if (Physics.Raycast(ray[i], out hit, 20))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    //Debug.Log(savedTime);
                    savedTime = 0;

                    animator.SetBool("IsDetected", true);
                    minion.SetBool("Walking", true);
                }
            }
            else
            {
                animator.SetBool("IsDetected", false);
                minion.SetBool("Walking", false);
            }
        }
    }

    private void CheckStartPos()
    {
        if (originalPos != transform.position)
        {
            animator.SetBool("IsNotAtStartingPos", true);
        }
        else
        {
            animator.SetBool("IsNotAtStartingPos", false);
        }
    }

    private void CheckIfInRange()
    {

        animator.SetFloat("DistanceFromPlayer", Vector3.Magnitude(player.transform.position - transform.position));

        if (animator.GetFloat("DistanceFromPlayer") < 2)
        {
            animator.SetBool("IsInRange", true);
            StartCoroutine(ChooseAttack());
        }
        else
        {
            animator.SetBool("IsInRange", false);
        }
    }

    private void CheckIfDead()
    {
        if (currentHP <= 0)
        {
            animator.SetBool("IsDead", true);
        }
        else 
        {
            animator.SetBool("IsDead", false);
        }
    }

    private void CheckIfRetreating()
    {
        if (currentHP <= (HP/2))
        {
            animator.SetBool("IsRetreating", true);
        }
        else
        {
            animator.SetBool("IsRetreating", false);
        }
    }

    IEnumerator CheckIfInvisible()
    {
        called = true;

        yield return new WaitForSeconds(10f);

        StartCoroutine(IsInvisible());
    }

    IEnumerator IsInvisible()
    {
        animator.SetBool("IsInvisible", true);

        yield return new WaitForSeconds(3f);

        animator.SetBool("IsInvisible", false);

        called = false;
    }

    IEnumerator ChooseAttack()
    {
        minion.SetBool("Attack", true);
            int rndm = UnityEngine.Random.Range(1, 4);
            if (rndm == 1)
                minion.SetInteger("Choose Attack", 1);
            if (rndm == 2)
                minion.SetInteger("Choose Attack", 2);
            if (rndm == 3)
                minion.SetInteger("Choose Attack", 3);
        yield return new WaitForSeconds(5f);
        minion.SetBool("Attack", false);
        
    }
    IEnumerator AttackDelay()
    {
        if (_playerManager != null)
        {
            _playerManager.TakeDamage(_damage);
        }
        yield return new WaitForSeconds(_attackDelay);
        _isAttacking = false;
    } 
      
}
