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
    private bool _isHealing;
    private bool _started;
    private EnemyController[] _list; 


    // Start is called before the first frame update
    void Start()
    {
        _isHealing = false;
        _started = false;
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
        if (_isHealing == false)
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
