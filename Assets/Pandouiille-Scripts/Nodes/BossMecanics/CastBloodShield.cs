using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastBloodShield : ActionNode
{
    public Transform playerTransform;
    public Transform bossTransform;
    public float shieldDuration = 10f;
    public float shieldTimer = 0f;

    protected override void OnStart()
    {
        //Activate Shield
    }
    protected override State OnUpdate()
    {
        shieldTimer += Time.deltaTime;

        if (shieldTimer >= shieldDuration)
        {
            //Deactivate Shield
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

}
