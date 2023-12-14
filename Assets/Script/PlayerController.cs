using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private DefaultPlayerAction _defaultPlayerAction;
    private InputAction _moveAction; 

    [SerializeField] private GameObject _camera;


    private float _moveSpeed = 5f;

    void Awake()
    {
       _defaultPlayerAction = new DefaultPlayerAction();
    }

    private void OnEnable()
    {
        _moveAction = _defaultPlayerAction.Player.Move;
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
    }

    private void FixedUpdate()
    {
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
        Vector3 movement = cameraForward * MoveDir.y + cameraRight * MoveDir.x;
        
        gameObject.GetComponent<Rigidbody>().velocity = movement * _moveSpeed;
        
    }
}
