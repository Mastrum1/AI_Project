using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNode
{
    public float duration = 5f;
    float StartTime;

    // Start is called before the first frame update
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        StartTime = Time.time;
    }

    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (Time.time - StartTime > duration)
        {
            return State.Success;
        }
        return State.Running;
    }
}
