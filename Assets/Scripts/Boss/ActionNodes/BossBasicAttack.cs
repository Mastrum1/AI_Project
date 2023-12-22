using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : ActionNode
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private int waveAmount;

    private float savedTime;
    // Start is called before the first frame update
    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        savedTime += Time.deltaTime;
        if (_projectile != null && savedTime >= 5f)
        {
            for (int i = 0; i < waveAmount; i++)
            {
                Instantiate(_projectile);
            }    
            savedTime = 0;
            return State.Success;
        }
        else return State.Running;
    }
}
