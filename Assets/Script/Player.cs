using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private DefaultPlayerAction _defaultPlayerAction;

    void Start()
    {
       _defaultPlayerAction = new DefaultPlayerAction();
    }

    private void OnEnable()
    {
        _defaultPlayerAction.Enable();
    }

    private void OnDisable()
    {
        _defaultPlayerAction.Disable();
    }

}
