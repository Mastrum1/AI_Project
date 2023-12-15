using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    public DefaultPlayerAction _defaultPlayerAction;
    private InputAction _moveAction;
    private InputAction _fire;
    private bool _fireReady;

    [SerializeField] private GameObject _playerBody;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _poi;
    [SerializeField] private GameObject _projectile;

    [SerializeField] private Animator _playerAnimations;
    [SerializeField] Rigidbody _rb;
 
    private float _moveSpeed = 5f;

    public Vector3 KnockBackDirection { get; private set; }


    void Awake()
    {
       _defaultPlayerAction = new DefaultPlayerAction();
       _fireReady = true;
    }

    private void OnEnable()
    {
        _moveAction = _defaultPlayerAction.Player.Move;
        _moveAction.Enable();
        _fire = _defaultPlayerAction.Player.Fire;
        _fire.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _fire.Disable();
    }

    private void FixedUpdate()
    {
        Mouvements();

        _playerBody.transform.LookAt(_poi.transform);
        Fire();
        _playerBody.transform.rotation = Quaternion.Euler(0, _playerBody.transform.rotation.eulerAngles.y, _playerBody.transform.rotation.eulerAngles.z);
    }

    private void Mouvements()
    {

        Debug.Log(_playerAnimations.GetBool("Walk"));
        // Get the camera's forward and right vectors
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Ignore the y-component to keep the movement in the horizontal plane
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors to ensure consistent movement speed in all directions
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector2 MoveDir = _moveAction.ReadValue<Vector2>();

        if (MoveDir.y > 0)
            _playerAnimations.SetBool("Walk", true);
        else if (MoveDir.y == 0)
            _playerAnimations.SetBool("Walk", false);

        if (MoveDir.y < 0)
            _playerAnimations.SetBool("Backward", true);
        else if (MoveDir.y == 0)
            _playerAnimations.SetBool("Backward", false);

        if (MoveDir.x > 0)
        {
            _playerAnimations.SetBool("StrafeLeft", false);
            _playerAnimations.SetBool("StrafeRight", true);
        }

        else if (MoveDir.x < 0)
        {
            _playerAnimations.SetBool("StrafeLeft", true);
            _playerAnimations.SetBool("StrafeRight", false);
        }

        else if (MoveDir.x == 0)
        {
            _playerAnimations.SetBool("StrafeRight", false);
            _playerAnimations.SetBool("StrafeLeft", false);
        }

        Vector3 movement = cameraForward * MoveDir.y + cameraRight * MoveDir.x;

        KnockBackDirection = Vector3.Lerp(KnockBackDirection, Vector3.zero, Time.fixedDeltaTime)/2;

        _rb.velocity = movement * _moveSpeed + KnockBackDirection;
    }
    public void Knockback(Vector3 dir)
    {
        KnockBackDirection = dir;
    }

    private void Fire()
    {
        float fire = _fire.ReadValue<float>();
        if ( gameObject.GetComponent<Player>().mana >= 10)
        {           
            if (fire > 0 && _fireReady)
            {
                gameObject.GetComponent<Player>().mana -= 10;
                GameObject myProj = Instantiate(_projectile, _playerBody.transform.position, _playerBody.transform.rotation);
                myProj.GetComponent<Rigidbody>().AddForce(_playerBody.transform.forward * 1000);
                StartCoroutine(FireCooldown());
            }
        }
        else
            _playerAnimations.SetBool("Attack", false);

    }

    IEnumerator FireCooldown()
    {
        _fireReady = false;
        yield return new WaitForSeconds(0.5f);
        _fireReady = true;
    }


}
