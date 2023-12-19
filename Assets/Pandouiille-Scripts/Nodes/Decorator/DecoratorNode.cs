using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorNode : Node
{
    [HideInInspector] public Node child;
    protected override void OnStart()
    {
    }
    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        State state = child.Update();
        return state;
    }

    public override Node Clone()
    {
        DecoratorNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }
}
