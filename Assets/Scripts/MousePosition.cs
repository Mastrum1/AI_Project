using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    private Vector3 _screenPosition; 
    private Vector3 _worldPosition;
    RaycastHit hit = new RaycastHit();
    [SerializeField] LayerMask mask;
    // Update is called once per frame

    void Update()
    {
        _screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(_screenPosition);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        if (Physics.Raycast(ray, out hit, 1000, mask)) 
        {
            _worldPosition = hit.point;
        }
        transform.position = _worldPosition;
    }
}
