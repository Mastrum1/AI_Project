using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileScript : MonoBehaviour
{
    private bool _isExploding;
    [SerializeField] private GameObject _explosion;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyProjectile());
        _isExploding = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isExploding = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _explosion.SetActive(true);
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(10f);
        if (gameObject && !_isExploding)
            Destroy(gameObject);
    }
}
