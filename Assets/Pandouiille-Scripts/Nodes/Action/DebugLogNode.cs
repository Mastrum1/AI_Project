using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string debugMessage = "Debug";

    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        Debug.Log($"OnStart{debugMessage}");
    }
    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        Debug.Log($"OnUpdate{debugMessage}");
        return State.Success;
    }
    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
        Debug.Log($"OnStop{debugMessage}");
    }

}
