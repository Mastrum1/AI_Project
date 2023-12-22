using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UnityEngine;

public class HealSpell : MonoBehaviour
{
    [SerializeField] private float _healAmount;
    [SerializeField] private float _healCooldown;
    [SerializeField] private float _healDuration;
    [SerializeField] private SphereCollider _collider;
    
    private GameObject _SpellLight;
    private bool _isHealing;
    private bool _started;
    private EnemyController[] _list;
    private bool _maxRadiusReach;


    // Start is called before the first frame update
    void Start()
    {
        _isHealing = false;
        _started = false;
        _maxRadiusReach = false;

        _SpellLight = GameObject.Find("HealingSpellEffects");
        if (_SpellLight != null)
            _SpellLight.SetActive(true); //a changer pour modifier l'effet du sort, activation instant simple for now
        else
            Debug.Log("HealingSpellEffects not found");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _list.Append(collision.gameObject.GetComponent<EnemyController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_started == false)
        {
            StartCoroutine(DestroyAura());
            _started = true;
        }

        if (_collider.radius < 1)
        {
            _collider.radius += 0.1f;
        }
        else
        {
            _maxRadiusReach = true;
        }

        if (_isHealing == false && _maxRadiusReach && _list != null)
        {
            foreach (EnemyController enemy in _list)
            {
                StartCoroutine(Heal());
            }
        }
    }

    IEnumerator DestroyAura()
    {
        yield return new WaitForSeconds(_healDuration);
        Destroy(gameObject);
    }

    IEnumerator Heal()
    {
        _isHealing = true;
        foreach (EnemyController enemy in _list)
        {
            enemy.Heal(_healAmount);
        }
        yield return new WaitForSeconds(_healCooldown);
        _isHealing = false;
    }
}
