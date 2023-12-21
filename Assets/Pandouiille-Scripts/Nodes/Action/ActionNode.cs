using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public string actionNodeMessage = "Action" ;
    protected override void OnStart()
    {
        Debug.Log($"OnStart{actionNodeMessage}");
    }
    protected override void OnStop()
    {
        Debug.Log($"OnStop{actionNodeMessage}");
    }

    protected override State OnUpdate()
    {
        Debug.Log($"OnUpdate{actionNodeMessage}");
        return State.Success;
    }

}
