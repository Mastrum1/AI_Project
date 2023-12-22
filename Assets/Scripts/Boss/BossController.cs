using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Player _playerS;

    public float HP;
    [NonSerialized] public float CurrentHp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_player.transform.position - transform.position, transform.up);
    }

    public void TakeDamage(float damage)
    {
        CurrentHp -= damage;
    }
}
