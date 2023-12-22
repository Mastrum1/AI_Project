using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallAttack : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private float _damage;
    [SerializeField] private float _duration;
    [SerializeField] private Player _playerManager;

    private bool _damageTook;
    private bool _active;

    // Start is called before the first frame update
    void Start()
    { 
        _active = false;
        _damageTook = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerManager.TakeDamage(_damage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_damageTook)
            {
                StartCoroutine(TakeDamage());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_active)
        {
            _active = true;
            StartCoroutine(WallCooldown());
        }
    }

    IEnumerator WallCooldown()
    {
        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
    }

    IEnumerator TakeDamage()
    {
        _damageTook = true;
        _playerManager.TakeDamage(_damage);
        yield return new WaitForSeconds(.01f);
        _damageTook = false;
    }
}
