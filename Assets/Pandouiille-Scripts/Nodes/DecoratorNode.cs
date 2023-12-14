using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorNode : Node
{
    public Node child;
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
}
