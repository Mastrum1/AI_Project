using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeNode : Node
{
    [HideInInspector] public List<Node> children = new List<Node>();

    protected override void OnStart()
    {

    }
    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.Running;
    }
    public override Node Clone()
    {
        CompositeNode node = Instantiate(this);
        node.children = children.ConvertAll(c => c.Clone());
        return node;
    }
}