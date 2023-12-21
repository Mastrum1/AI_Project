using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastBloodSea : ActionNode
{
    public float interval = 10f;
    private float Timer = 0f;

    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        Timer = interval;
    }
    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            //Cast Blood Sea
            Timer = interval;
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
