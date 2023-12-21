using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageAttack : ActionNode
{
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        
    }
    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        return State.Success;
    }
}
