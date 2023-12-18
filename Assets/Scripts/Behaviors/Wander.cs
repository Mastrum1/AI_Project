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
    private bool flag;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<EnemyController>().agent;
        floorBounds = areaBound.GetComponent<MeshRenderer>().bounds;
        user = animator.gameObject;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.hasPath == false && flag == false)
        {
            flag = true;
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        do
        {
            float minX = floorBounds.size.x * -0.5f;
            float minZ = floorBounds.size.z * -0.5f;

            randDestination = user.transform.TransformPoint(new Vector3(Random.Range(minX, -minX), 
                user.transform.position.y, Random.Range(minZ, -minZ)));

            Debug.Log("rand:" + randDestination);
        }
        while (CheckIfOnNav());

        agent.SetDestination(randDestination);

        flag = false;
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
