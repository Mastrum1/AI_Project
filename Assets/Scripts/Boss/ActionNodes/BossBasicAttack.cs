using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : ActionNode
{
    [SerializeField] private GameObject _projectile;

    private float savedTime;
    // Start is called before the first frame update
    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        savedTime += Time.deltaTime;
        if (_projectile != null && savedTime >= 3f)
        {
            Instantiate(_projectile);
            savedTime = 0;
            return State.Success;
        }
        else return State.Failure;
    }
}
