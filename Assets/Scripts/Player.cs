using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int CountKey;
    private float _maxHp;
    public float hp;
    private float _maxMana;
    public float mana;
    private bool _manaRegen = false;
    private bool _HealRegen = false;
    public bool CursedMod = false;

    [SerializeField] private GameObject UI;
    [SerializeField] private Volume globalVolume;
    [SerializeField] private CinemachineFreeLook _cam;
    private ChromaticAberration ca;
    CinemachineBasicMultiChannelPerlin _noise;
    private Image health;

    // Start is called before the first frame update
    void Awake()
    {
        CountKey = 0;
        _maxHp = hp;
        _maxMana = mana;
        health = UI.GetComponent<Image>();
        globalVolume.profile.TryGet<ChromaticAberration>(out ca);

        _noise = _cam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_manaRegen && mana < _maxMana)
            StartCoroutine(ManaRegen());
        if (!_HealRegen && hp < _maxHp)
            StartCoroutine(HealRegen());
        if (hp <= 0)
        {
            hp = 0;
        }
            
        float _hpLossPercent = 1 - (hp / _maxHp);
        ca.intensity.value = _hpLossPercent;

        if (_hpLossPercent > 0.5f)
        {
            var tempColor = health.color;
            tempColor.a = _hpLossPercent;
            health.color = tempColor;
        }
        else
        {
            var tempColor = health.color;
            if (tempColor.a > 0f)
            {
                tempColor.a = health.color.a-.001f;
            }
            health.color = tempColor;
        }

        switch (hp) 
        { 
            case 0:
                if (CursedMod)
                {
                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
                }
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        StartCoroutine(Shake(.3f));
    }

    IEnumerator Shake(float time)
    {
        _noise.m_AmplitudeGain = 1;
        yield return new WaitForSeconds(time);
        _noise.m_AmplitudeGain = 0;
    }

    IEnumerator ManaRegen()
    {
        _manaRegen = true;
        yield return new WaitForSeconds(.5f);
        mana ++;
        _manaRegen = false;
    }
    IEnumerator HealRegen()
    {
        _HealRegen = true;
        yield return new WaitForSeconds(3f);
        hp ++;
        _HealRegen = false;
    }
}
