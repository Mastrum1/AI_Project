using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float fovAngle;
    [SerializeField] private Player _playerManager;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _damage;
    [NonSerialized] public Vector3 originalPos;
    [NonSerialized] public float currentHP;

    public GameObject player;
    public NavMeshAgent agent;
    public float HP;
    public float detectionTime;

    private float savedTime;
    private bool _isAttacking = false;
    private Animator animator;
    public Animator minion;
    private float currentHP;
    private bool calledInvis;
    private RaycastHit hit;
    private Ray[] ray;

    void Start()
    {
        animator = GetComponent<Animator>();
        originalPos = transform.position;
        currentHP = HP;

        CreateNewFieldOfView();
    }

    // Update is called once per frame
    void Update()
    {
        savedTime += Time.deltaTime;

        CheckIfDead();

        if (savedTime >= 3f)
        {
            CreateNewFieldOfView();
        }

        CheckRayHit();

        CheckIfInRange();

        Attack.OnAttack += Attacking;

        if (gameObject.tag == "Skeleton" && gameObject.tag == "Berserk")
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

    private void CreateNewFieldOfView()
    {
        ray = new Ray[2];
        savedTime = 0;

        ray[0] = new Ray(transform.position, transform.forward);

        for (int i = 1; i < ray.Length; i++)
        {
            float rand = UnityEngine.Random.Range(-fovAngle, fovAngle);

            ray[i] = new Ray(Quaternion.AngleAxis((rand / 2), transform.up) * transform.forward, transform.forward);
        }
    }

    private void CheckRayHit()
    {
        for (int i = 0; i < ray.Length - 1; i++)
        {
            if (Physics.Raycast(ray[i], out hit, 20))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
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
        if (gameObject.tag == "Berserker" && currentHP / HP * 100 > 50)
        {

            for (int i = 0; i < ray.Length - 1; i++)
            {
                if (Physics.Raycast(ray[i], out hit, 5))
                {
                    if (hit.transform.gameObject.tag == "Player")
                    {
                        animator.SetBool("IsInRange", true);
                        StartCoroutine(ChooseAttack());
                    }
                }
                else
                {
                    animator.SetBool("IsInRange", false);
                }
            }
        }
        else
        {
            if (Vector3.Magnitude(player.transform.position - transform.position) < 2)
            {
                animator.SetBool("IsInRange", true);
                StartCoroutine(ChooseAttack());
            }
            else
            {
                animator.SetBool("IsInRange", false);
            }
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
