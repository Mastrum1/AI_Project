using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public string actionNodeMessage = "Action" ;
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        Debug.Log($"OnStart{actionNodeMessage}");
    }
    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
        Debug.Log($"OnStop{actionNodeMessage}");
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        Debug.Log($"OnUpdate{actionNodeMessage}");
        return State.Success;
    }
}
