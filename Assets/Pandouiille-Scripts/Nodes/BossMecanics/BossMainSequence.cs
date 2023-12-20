using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BossMainSequence : DecoratorNode
{
    [HideInInspector] public int playerAttack;
    [HideInInspector] public int bossHealth;
    [HideInInspector] public int bossMaxHealth;
    
    protected override void OnStart()
    {
    }
    protected override State OnUpdate()
    {
        if (playerAttack >= bossHealth * 0.1)
        {
            //ActivateShieldSpell
        }
        if (bossHealth <= bossMaxHealth * 0.75)
        {
            //ActivateBloodSeaSpell
        }
        return State.Success;
    }

    protected override void OnStop()
    {
    }
}
