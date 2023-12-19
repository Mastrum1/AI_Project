using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Wander : StateMachineBehaviour
{
    [SerializeField] private GameObject areaBound;

    private Bounds floorBounds;
    private NavMeshAgent agent;
    private GameObject user;
    private Vector3 randDestination;
    float time;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<EnemyController>().agent;
        floorBounds = areaBound.GetComponent<MeshRenderer>().bounds;
        user = animator.gameObject;

        if (agent.hasPath == false)
        {
            SetRandomDestination();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;

        if (agent.hasPath == false && time > 1.5)
        {
            time = 0;
            animator.SetBool("IsInspecting", true);
        }
    }

    private void SetRandomDestination()
    {
        do
        {
            float X = Random.Range(floorBounds.min.x, floorBounds.max.x);
            float Z = Random.Range(floorBounds.min.z, floorBounds.max.z);

            randDestination = user.transform.TransformPoint(new Vector3( X,
                user.transform.position.y, Z));

            agent.SetDestination(randDestination);

            Debug.Log("rand:" + randDestination);

        } while (CheckIfOnNav());
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
