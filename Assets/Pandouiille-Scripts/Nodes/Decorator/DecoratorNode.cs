using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorNode : Node
{
    [HideInInspector] public Node child;
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

    public override Node Clone()
    {
        DecoratorNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }
}
