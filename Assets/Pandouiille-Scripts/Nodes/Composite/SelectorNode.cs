using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    int current;

    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        current = 0;
    }

    protected override void OnStop(BehaviourTreeLauncher launcher)
    {

    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        for (int i = 0; i < children.Count; i++)
        {
            current = i;
            var child = children[current];
            switch (child.MyUpdate(launcher))
            {
                case State.Running:
                    return State.Running;
                case State.Success:
                    return State.Success;
                case State.Failure:
                    continue;
            }
        }
        return State.Failure;
    }
}
