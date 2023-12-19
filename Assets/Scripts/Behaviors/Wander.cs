using System.Collections;
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
    private bool inspecting;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<EnemyController>().agent;
        floorBounds = areaBound.GetComponent<MeshRenderer>().bounds;
        user = animator.gameObject;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.hasPath == false && flag == false && inspecting == false)
        {
            flag = true;
            SetRandomDestination();
        }

        if (inspecting == true)
        {
            InspectSuroundings();
        }
    }

    private void SetRandomDestination()
    {
        do
        {
            float minX = floorBounds.size.x * -0.5f;
            float minY = floorBounds.size.y * -0.5f;
            float minZ = floorBounds.size.z * -0.5f;

            var randomPoint = this.user.transform.TransformPoint(
                new Vector3(Random.Range(minX, -minX), Random.Range(minY, -minY), Random.Range(minZ, -minZ)));

            agent.SetDestination(randomPoint);

            //pole.transform.position = new Vector3(randomPoint.x, user.transform.position.y, randomPoint.z);
        }
        while (CheckIfOnNav());

        flag = false;
        //inspecting = true;
    }

    private bool CheckIfOnNav()
    {
        if (agent.pathEndPosition != randDestination)
        {
            return false;
        }
        return true;
    }

    private void InspectSuroundings()
    {
        Quaternion myRot = user.transform.rotation;
        myRot.x += 20;// ta valeur calculé

        user.transform.rotation = myRot;
    }
}
