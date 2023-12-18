using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [SerializeField] GameObject _chest;
    [SerializeField] float translateSpeed = 1f;
    void Start()
    {
        Vector3 mouvement = new Vector3(0, 1f, 0) * translateSpeed * Time.deltaTime;
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(IncreaseRotation());
/*            collision.GetComponent<Player>().CountKey++;
*/        }
    }

    private IEnumerator IncreaseRotation()
    {
        float targetRotX = _chest.transform.rotation.eulerAngles.x - 45f;
        float rotationSpeed = -50f;
        while (_chest.transform.rotation.eulerAngles.x > targetRotX)
        {
            float newXRotation = _chest.transform.rotation.eulerAngles.x + rotationSpeed * Time.deltaTime;
            _chest.transform.rotation = Quaternion.Euler(newXRotation, 270f, 0f);

            yield return null;
        }
        gameObject.gameObject.SetActive(false);

    }
}
