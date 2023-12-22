using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallAttack : ActionNode
{
    [SerializeField] private GameObject _wallAttack;
    // Start is called before the first frame update
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        Instantiate(_wallAttack);
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (_wallAttack != null)
        {
            return State.Success;
        }
        else return State.Failure;
    }
}
