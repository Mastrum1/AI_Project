using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GoToPlayer : ActionNode
{
    private NavMeshAgent agent;
    private EnemyController user;
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        agent = launcher.GetComponent<NavMeshAgent>();
        user = launcher.GetComponent<EnemyController>();
    }
    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        agent.SetDestination(user.player.transform.position);

        user.transform.rotation = Quaternion.LookRotation(user.player.transform.position - user.transform.position, user.transform.up);

        if (Vector3.Magnitude(user.player.transform.position - user.transform.position) < 2)
        {
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }
}