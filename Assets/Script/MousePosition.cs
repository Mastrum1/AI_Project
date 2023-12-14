using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    private Vector3 _screenPosition; 
    private Vector3 _worldPosition;
    Plane plane = new Plane(Vector3.down, Vector3.zero);
    // Update is called once per frame
    void Update()
    {
        _screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(_screenPosition);

        if (plane.Raycast(ray, out float distance)) 
        {
            _worldPosition = ray.GetPoint(distance);
        }
        transform.position = _worldPosition;
    }
}
