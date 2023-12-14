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
 
    private float _moveSpeed = 5f;

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
        Fire();

        _playerBody.transform.LookAt(_poi.transform);
        
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

        gameObject.GetComponent<Rigidbody>().velocity = movement * _moveSpeed;

  
    }

    private void Fire()
    {
        float fire = _fire.ReadValue<float>();
        if (fire > 0 && _fireReady)
        {
            _playerAnimations.SetBool("Attack", true);
            GameObject myProj = Instantiate(_projectile, transform.position, _playerBody.transform.rotation);
            myProj.GetComponent<Rigidbody>().AddForce(_playerBody.transform.forward * 1000);
            StartCoroutine(FireCooldown());
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
