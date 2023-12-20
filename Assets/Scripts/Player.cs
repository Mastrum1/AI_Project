using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Rendering;
using UnityEngine;
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
    private Image health;

    // Start is called before the first frame update
    void Awake()
    {
        CountKey = 0;
        _maxHp = hp;
        _maxMana = mana;
       health = UI.GetComponent<Image>();
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
            
        float hpLossPercent = 1 - (hp / _maxHp);
        if (hpLossPercent > 0.5f)
        {
            var tempColor = health.color;
            tempColor.a = hpLossPercent;
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
