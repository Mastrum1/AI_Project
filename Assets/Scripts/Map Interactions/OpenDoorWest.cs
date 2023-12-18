using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject _door;
    [SerializeField] float translateSpeed = 1f;
    void Start()
    {
        Vector3 mouvement = new Vector3(0,1f,0) * translateSpeed * Time.deltaTime;
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            //ChangeTargetObjectPosition();
            StartCoroutine(IncreaseRotation());
        }
    }

    void ChangeTargetObjectPosition()
    {
        if (_door != null)
        {
            if (_door.transform.rotation.y < 105)
            {
                Quaternion newPosition = _door.transform.rotation;
                newPosition.y = 0.1f;
                _door.transform.rotation = newPosition;
            }
        }
    }

    private IEnumerator IncreaseRotation() 
    {
        float targetRotY = _door.transform.rotation.eulerAngles.y + 105f;
        float rotationSpeed = 50f;
        while (_door.transform.rotation.eulerAngles.y < targetRotY)
        {
            float newYRotation = _door.transform.rotation.eulerAngles.y + rotationSpeed * Time.deltaTime;
            _door.transform.rotation = Quaternion.Euler(0f, newYRotation, 0f);

            yield return null;
        }
        gameObject.gameObject.SetActive(false);
       
    }
}
