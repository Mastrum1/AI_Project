using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileScript : MonoBehaviour
{
    private bool _isExploding;
    private bool _isDestroying;

    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private GameObject _trail;
    [SerializeField] private GameObject _light;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyProj());
        _isExploding = false;
        _isDestroying = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isExploding = true;
        Destroy(_vfx);
        Destroy(_trail);

        if (collision.gameObject.name == "Door")
        {
            _isDestroying = true;
            StartCoroutine(DestroyDoor(collision.gameObject));           
        }

        _collider.enabled = false;
        _mesh.enabled = false;
        _rb.velocity = Vector3.zero;
        _explosion.SetActive(true);
        if (!_isDestroying)
            Destroy(gameObject, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
       if (_isExploding)
        {
            _light.GetComponent<Light>().intensity -= 0.22f;
        }
    }

    void OnCollision(Collision collision)
    {
        UnityEngine.Debug.Log(collision.gameObject.name);
    }

    IEnumerator DestroyProj()
    {
        if (!_isExploding && !_isDestroying && gameObject!= null)
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyDoor(GameObject door)
    {
        if (door != null)
        {
            door.GetComponent<Rigidbody>().isKinematic = false;
            door.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 100f;
            door.transform.GetChild(0).gameObject.SetActive(false);
            door.GetComponent<Door>().isBurning = true;
            yield return new WaitForSeconds(2f);
            Destroy(door);
            Destroy(gameObject);
        }
    }
}
