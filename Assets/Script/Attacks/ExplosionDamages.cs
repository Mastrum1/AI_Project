using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamages : MonoBehaviour
{
    [SerializeField] private SphereCollider _area;
    private float _maxRadius = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _maxRadius = _area.radius;
        _area.radius = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject entity = other.gameObject;
        switch (other.gameObject.tag)
        {
            case "Player":
                if (entity.TryGetComponent<PlayerController>(out var pc))
                {
                    pc.Knockback((entity.transform.position - transform.position)* 10f);
                }
                entity.GetComponent<Player>().hp -= 10;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_area.radius < _maxRadius)
            _area.radius += 0.05f;
    }
}
