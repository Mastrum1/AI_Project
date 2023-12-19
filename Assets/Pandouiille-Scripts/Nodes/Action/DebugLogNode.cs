using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string debugMessage = "Debug";

    protected override void OnStart()
    {
        Debug.Log($"OnStart{debugMessage}");
    }
    protected override void OnStop()
    {
        Debug.Log($"OnStop{debugMessage}");
    }

    protected override State OnUpdate()
    {
        Debug.Log($"OnUpdate{debugMessage}");
        return State.Success;
    }
}
