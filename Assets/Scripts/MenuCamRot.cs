using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamRot : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cam;

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
            cam.m_XAxis.Value += 0.1f;  
    }
}
