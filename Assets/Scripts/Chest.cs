using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;

    public float VanishValue = -1f;
    float VanishGradient = 0.1f;
    bool isVanishing = false;
    public bool isBurning = false;

    private void Update()
    {
        if (isBurning)
        {
            DisolveDoor();
        }
    }
    public void DisolveDoor()
    {
        if (mesh != null)
        {
            if (VanishValue < 1f && !isVanishing)
            {
                VanishValue += VanishGradient;
                StartCoroutine(Vanish(VanishValue));
            }
        }
    }

    IEnumerator Vanish(float VanishValue)
    {
        isVanishing = true;
        mesh.material.SetFloat("_Value", VanishValue);
        yield return new WaitForSeconds(0.05f);
        isVanishing = false;
    }
}