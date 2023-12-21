using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : DecoratorNode
{
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        
    }
    protected override void OnStop(BehaviourTreeLauncher launcher)
    {

    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        State state = child.MyUpdate(launcher);
        return state;
    }
}
