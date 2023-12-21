using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject _door;
    [SerializeField] Player _player;
    [SerializeField] float translateSpeed = 1f;
    void Start()
    {
        Vector3 mouvement = new Vector3(0,1f,0) * translateSpeed * Time.deltaTime;
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider colision)
    {
        if (colision.tag == "Player" && gameObject.tag != "Boss")
        {
            StartCoroutine(IncreaseRotation());
        }
        else if (colision.tag == "Player" && gameObject.tag == "Boss")
        {
            if (_player.CountKey == 3) 
            {
                StartCoroutine(IncreaseRotation());
                StartCoroutine(StartBoss());
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

    private IEnumerator StartBoss()
    {
        yield return new WaitForSeconds(2f);
        GameObject.Find("SceneManager").GetComponents<myScenesManager>()[0].StartBoss();
    }
}
