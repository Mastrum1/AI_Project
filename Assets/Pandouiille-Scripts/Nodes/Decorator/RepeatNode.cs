using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : DecoratorNode
{
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
    }

    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        child.MyUpdate(launcher);
        return State.Running;
    }
}
