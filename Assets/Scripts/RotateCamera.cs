using Cinemachine;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class RotateCamera : MonoBehaviour
{
    private DefaultPlayerAction _defaultPlayerAction;

    InputAction _middleClick;
    InputAction _Keyboard;
    InputAction _deltaMouse;

    public float RotationStrengh = 1;
    private bool _isMiddleClickPressed;

    private void Start()
    {
        _isMiddleClickPressed = false;

        _defaultPlayerAction = GameObject.Find("Player").GetComponent<PlayerController>()._defaultPlayerAction;

        _middleClick = _defaultPlayerAction.Player.UnlockFreeLook;
        _middleClick.Enable();
        _deltaMouse = _defaultPlayerAction.Player.LookMouse;
        _deltaMouse.Enable();
        _Keyboard = _defaultPlayerAction.Player.Look;
        _Keyboard.Enable();

    }

    private void OnDisable()
    {
        _middleClick.Disable();
        _deltaMouse.Disable();
        _Keyboard.Disable();
    }

    private void Update()
    {
        switch (_middleClick.ReadValue<float>()) 
        {
            case 0:
                _isMiddleClickPressed = false;
                break;
            case 1:
                _isMiddleClickPressed = true;
                break;
        }

        gameObject.GetComponent<CinemachineFreeLook>().m_XAxis.Value += _Keyboard.ReadValue<Vector2>().x*RotationStrengh;
        if (_isMiddleClickPressed)
            gameObject.GetComponent<CinemachineFreeLook>().m_XAxis.Value += _deltaMouse.ReadValue<float>()/4;
    }
}
