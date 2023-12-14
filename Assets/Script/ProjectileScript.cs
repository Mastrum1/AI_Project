using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileScript : MonoBehaviour
{
    private bool _isExploding;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private GameObject _trail;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyProj());
        _isExploding = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isExploding = true;
        Destroy(_vfx);
        Destroy(_trail);
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

    IEnumerator DestroyProj()
    {
        if (!_isExploding && gameObject!= null)
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
    }
}
