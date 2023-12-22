using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _entityRenderer;
    [SerializeField] private GameObject _entityPhysics;
    [SerializeField] private Rigidbody _entityRb;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _respawnTime;
    [SerializeField] float fovAngle;
    [SerializeField] private Player _playerManager;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _damage;

    [NonSerialized] public Vector3 originalPos;
    [NonSerialized] public bool playerDetected;

    public float _currentHp { get; private set; }
    public GameObject player;
    public NavMeshAgent agent;
    public float detectionTime;
    public float maxHp;

    private bool _isDead;
    private float savedTime;
    private bool _isAttacking;
    private Animator animator;
    public Animator minion;
    private bool calledInvis;
    private RaycastHit hit;
    private Ray[] ray;

    void Start()
    {
        _isDead = false;
        if (gameObject.tag != "Mage")
        {
            animator = GetComponent<Animator>();
        }
        originalPos = transform.position;
        _currentHp = maxHp;
        _isAttacking = false;
        playerDetected = false;

        CreateNewFieldOfView();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_currentHp);
        savedTime += Time.deltaTime;

        if (gameObject.tag != "Mage")
        {
            if (savedTime >= 3f)
            {
                CreateNewFieldOfView();
            }

            CheckRayHit();

            CheckIfDead();

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
        if (gameObject.tag == "Berserker" && _currentHp / maxHp * 100 > 50)
        {

            for (int i = 0; i < ray.Length - 1; i++)
            {
                if (Physics.Raycast(ray[i], out hit, 5))
                {
                    if (hit.transform.gameObject.tag == "Player")
                    {
                        animator.SetBool("IsInRange", true);
                        AudioManager.instance.PlaySFX("Enemy Attack");
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

    public void Heal(float value)
    {
        _currentHp += value;
    }   

    private void CheckIfDead()
    {
        if (_currentHp <= 0 && !_isDead)
        {
            _isDead = true;
            animator.SetBool("IsDead", true);
            StartCoroutine(Resurect());
        }
    }

    private void CheckIfRetreating()
    {
        if (_currentHp <= (maxHp/2))
        {
            animator.SetBool("IsRetreating", true);
        }
        else
        {
            animator.SetBool("IsRetreating", false);
        }
    }

    //event � modifier
    private void Attacking()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            if (gameObject.tag == "Berserker" && _currentHp >= (maxHp / 2))
            {
                RangedAttack();
            }
            else CacAttack();

            StartCoroutine(AttackDelay());
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
    }

    public void CacAttack()
    {
        if (!_isDead && !_isAttacking)
        {
            _isAttacking = true;
            _playerManager.TakeDamage(_damage);
            StartCoroutine(AttackDelay());
        }
    }
    public void RangedAttack()
    {
        if (!_isDead && !_isAttacking)
        {
            _isAttacking = true;
            Instantiate(_projectile, transform.position, transform.rotation);
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
        yield return new WaitForSeconds(_attackDelay);
        _isAttacking = false;
    }

    IEnumerator Resurect()
    {
        _entityRb.velocity = Vector3.zero;
        minion.SetBool("IsDead", true); 
        yield return new WaitForSeconds(_respawnTime);
        _currentHp = maxHp;
        _isDead = false;
        minion.SetBool("IsDead", false);
        animator.SetBool("IsDead", false);
        _entityPhysics.SetActive(true);
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            if (gameObject.tag != "Mage")
            {
                animator.SetBool("IsDetected", true);
            }
            else
            {
                playerDetected = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag != "Mage")
            {
                animator.SetBool("IsDetected", true);
            } 
            else
            {
                playerDetected = false;
            }
        }
    }
}

