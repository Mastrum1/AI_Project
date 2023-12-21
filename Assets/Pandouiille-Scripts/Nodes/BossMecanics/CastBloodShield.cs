using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastBloodShield : ActionNode
{
    public Transform playerTransform;
    public Transform bossTransform;
    public float shieldDuration = 10f;
    public float shieldTimer = 0f;

    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        Debug.Log("Blood Shield On");
        //Activate Shield
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        shieldTimer += Time.deltaTime;

        if (shieldTimer >= shieldDuration)
        {
            Debug.Log("Blood Shield Off");
            //Deactivate Shield
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }

    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
        base.OnStop(launcher);
    }

}
