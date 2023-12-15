using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    private Vector3 _screenPosition; 
    private Vector3 _worldPosition;
    RaycastHit hit = new RaycastHit();
    // Update is called once per frame
    void Update()
    {
        _screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(_screenPosition);

        if (Physics.Raycast(ray, out hit)) 
        {
            _worldPosition = hit.point;
        }
        transform.position = _worldPosition;
    }
}
