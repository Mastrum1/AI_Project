using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _maxHp;
    public float hp;
    private float _maxMana;
    public float mana;
    private bool _manaRegen = false;
    private bool _HealRegen = false;
    public bool CursedMod = false;

    // Start is called before the first frame update
    void Awake()
    {
        _maxHp = hp;
        _maxMana = mana;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_manaRegen && mana < _maxMana)
            StartCoroutine(ManaRegen());
        if (!_HealRegen && hp < _maxHp)
            StartCoroutine(HealRegen());

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
