using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Wander : StateMachineBehaviour
{
    [SerializeField] private NavMeshSurface surface;

    private NavMeshData surfaceData;
    private NavMeshAgent agent;
    private GameObject user;
    private Vector3 randDestination;
    private bool finished;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<EnemyController>().agent;
        surfaceData = surface.navMeshData;
        user = animator.gameObject;
        finished = false;
        SetRandomDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.hasPath == false && finished == true)
        {
            animator.SetBool("IsInspecting", true);
        }
    }

    private void SetRandomDestination()
    {
        do
        {
            float X = Random.Range(surfaceData.sourceBounds.min.x, surfaceData.sourceBounds.max.x);
            float Z = Random.Range(surfaceData.sourceBounds.min.z, surfaceData.sourceBounds.max.z);

            randDestination = user.transform.TransformPoint(new Vector3(X, user.transform.position.y, Z));

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
