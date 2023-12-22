using System.Collections;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private GameObject _player;
    private bool _isExploding;
    private bool _isDestroying;

    [SerializeField] private float _power;
    [SerializeField] private Player _playerS;
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
        _player = GameObject.Find("Player");

        transform.LookAt(_player.transform);
        _rb.velocity = transform.forward * _power;

        StartCoroutine(DestroyProj());
        _isExploding = false;
        _isDestroying = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isExploding = true;
        Destroy(_vfx);
        Destroy(_trail);

        _collider.enabled = false;
        _mesh.enabled = false;
        _rb.velocity = Vector3.zero;
        _explosion.SetActive(true);
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

    IEnumerator DestroyProj()
    {
        if (!_isExploding && !_isDestroying && gameObject != null)
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
    }
}
