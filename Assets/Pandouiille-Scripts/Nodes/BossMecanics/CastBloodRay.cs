using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastBloodRay : ActionNode
{
    public Transform playerTransform;
    public Transform bossTransform;
    public float castDistance = 5f;
    public float RayDuration = 5f;
    public float RayTimer = 0f;
    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bossTransform = GameObject.FindGameObjectWithTag("Boss").transform;
    }

    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
        base.OnStop(launcher);
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        RayTimer += Time.deltaTime;
        if (Vector3.Distance(playerTransform.position, bossTransform.position) < castDistance)
        {
            if (RayTimer >= RayDuration)
            {
                //Casting Blood Ray
                return State.Success;
            }
            else
            {
                return State.Running;
            }
        }
        else
        {
            return State.Failure;
        }
    }
}
