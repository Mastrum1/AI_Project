using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerHealth : DecoratorNode
{
    public GameObject player;
    private float currentHealth;
    public float healthPercentage = 0.5f;

    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        //float currentHealth = player.GetComponent<PlayerHealth>().currentHealth;
        //float maxHealth = player.GetComponent<PlayerHealth>().maxHealth;
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (!player)
        {
            return State.Failure;
        }

        if (currentHealth > healthPercentage)
        {
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }

    protected override void OnStop(BehaviourTreeLauncher launcher)
    {
    }
}
