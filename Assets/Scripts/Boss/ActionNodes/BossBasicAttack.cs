using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : ActionNode
{
    [SerializeField] private GameObject _projectile;
    // Start is called before the first frame update
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
       Instantiate(_projectile);
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (_projectile != null)
        {
            return State.Success;
        }
        else return State.Failure;
    }
}
