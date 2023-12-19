using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : DecoratorNode
{
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
