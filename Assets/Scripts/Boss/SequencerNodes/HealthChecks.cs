using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChecks : SequencerNode
{
    [SerializeField] private BossController _bossController;
    private float HP;
    private float currentHP;
    // Start is called before the first frame update
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        HP = _bossController.HP;
        currentHP = _bossController.CurrentHp;
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (currentHP <= HP * 0.75 && currentHP >= HP * 0.6)
        {
            return State.Success;
        }
        else if (currentHP <= HP * 0.5 && currentHP >= HP * 0.35)
        {
            return State.Success;
        }
        else if (currentHP <= HP * 0.2 && currentHP >= HP * 0.1)
        {
            return State.Success;
        }
        else return State.Failure;
    }
}
