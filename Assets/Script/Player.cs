using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _hp;
    public bool _hardcoreMod = false;
    // Start is called before the first frame update
    void Awake()
    {
        _hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_hp) 
        { 
            case 0:
                if (_hardcoreMod)
                {
                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
                }
                break;
        }
        _hp -= 1;
    }
}
