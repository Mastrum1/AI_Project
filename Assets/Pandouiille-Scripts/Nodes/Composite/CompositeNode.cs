using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeNode : Node
{
    [HideInInspector] public List<Node> children = new List<Node>();

    protected override void OnStart(BehaviourTreeLauncher launcher)
    {

    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        return State.Running;
    }

    protected override void OnStop(BehaviourTreeLauncher launcher)
    {

    }

    public override Node Clone()
    {
        CompositeNode node = Instantiate(this);
        node.children = children.ConvertAll(c => c.Clone());
        return node;
    }
}
