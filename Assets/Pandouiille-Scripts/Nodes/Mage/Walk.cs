using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Walk : ActionNode
{
    [SerializeField] private NavMeshSurface surface;

    private NavMeshAgent agent;
    private NavMeshData surfaceData;
    private Vector3 randDestination;
    private bool finished;
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        agent = launcher.GetComponent<NavMeshAgent>();
        surfaceData = surface.navMeshData;
        finished = false;

        SetRandomDestination(launcher);
    }
    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (agent.hasPath == false && finished == true)
        {
            return State.Success;
        }
        else return State.Running;
    }
    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
        Debug.Log($"OnStop{actionNodeMessage}");
    }

    private void SetRandomDestination(BehaviourTreeLauncher launcher)
    {
        do
        {
            float X = Random.Range(surfaceData.sourceBounds.min.x, surfaceData.sourceBounds.max.x);
            float Z = Random.Range(surfaceData.sourceBounds.min.z, surfaceData.sourceBounds.max.z);
            randDestination = launcher.transform.TransformPoint(new Vector3(X, launcher.transform.position.y, Z));

            agent.SetDestination(randDestination);

        } while (CheckIfOnNav());

        finished = true;
    }

    private bool CheckIfOnNav()
    {
        if (agent.pathEndPosition != randDestination)
        {
            return false;
        }
        return true;
    }
}
