using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
    public Node child;
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
    }

    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        return child.MyUpdate(launcher);
    }

    public override Node Clone()
    {
        RootNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }

}
