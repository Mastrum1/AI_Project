using UnityEngine;

public class CastBloodOrb : ActionNode
{
    private Transform playerTransform;
    private Transform bossTransform;
    public float castDistance = 5f;

    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bossTransform = launcher.transform;
    }

    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
        base.OnStop(launcher);
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (Vector3.Distance(playerTransform.position, bossTransform.position) < castDistance)
        {
            Debug.Log("Cast Blood Orb");
            //Casting Blood Orb
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
