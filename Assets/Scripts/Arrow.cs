using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _power;
    [SerializeField] private float _damage;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(_player.transform);
        _rb.velocity = transform.forward * _power;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
