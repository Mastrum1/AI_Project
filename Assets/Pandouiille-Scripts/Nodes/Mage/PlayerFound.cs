using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFound : SequencerNode
{
    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (launcher.gameObject.GetComponent<EnemyController>().playerDetected)
        {
            return State.Success;
        }
        else return State.Failure;
    }
}