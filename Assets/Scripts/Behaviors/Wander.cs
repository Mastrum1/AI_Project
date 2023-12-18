using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Wander : StateMachineBehaviour
{
    [SerializeField] GameObject areaBound;
    [SerializeField] GameObject pole;

    private Bounds floorBounds;
    private NavMeshAgent agent;
    private GameObject user;
    private Vector3 randDestination;
    private bool flag;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<EnemyController>().agent;
        floorBounds = areaBound.GetComponent<Renderer>().bounds;
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
        float x = Random.Range(floorBounds.min.x, floorBounds.max.x);
        float z = Random.Range(floorBounds.min.z, floorBounds.max.z);

        randDestination = new Vector3(x, user.transform.position.y, z);

        agent.SetDestination(randDestination);

        pole.transform.position = new Vector3(x, user.transform.position.y, z);

        CheckIfOnNav();

        flag = false;
    }

    private void CheckIfOnNav()
    {
        if (agent.pathEndPosition != randDestination)
        {
            SetRandomDestination();
        }
        else return;
    }
}
