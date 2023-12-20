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
    [SerializeField] private Player _playerManager;
    [NonSerialized] public float savedTime;
    [NonSerialized] public Vector3 originalPos;

    [SerializeField] private float _attackDelay;
    [SerializeField] private float _damage;

    private bool _isAttacking = false;
    private Animator animator;
    private float currentHP;
    private bool calledInvis;
    private bool calledInspec;

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
        Attack.OnAttack += Attacking;

        CheckIfDead();

        CreateFieldOfView();

        CheckRayHit();

        CheckIfInRange();

        if (gameObject.tag == "Skeleton")
        {
            CheckStartPos();
        }

        if (gameObject.tag == "Assassin")
        {
            CheckIfRetreating();

            if (!calledInvis)
            {
                StartCoroutine(CheckIfInvisible());
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
                }
            }
            else
            {
                animator.SetBool("IsDetected", false);
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

    private void Attacking()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;

            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator CheckIfInvisible()
    {
        calledInvis = true;

        yield return new WaitForSeconds(10f);

        StartCoroutine(IsInvisible());
    }

    IEnumerator IsInvisible()
    {
        animator.SetBool("IsInvisible", true);

        yield return new WaitForSeconds(3f);

        animator.SetBool("IsInvisible", false);

        calledInvis = false;
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