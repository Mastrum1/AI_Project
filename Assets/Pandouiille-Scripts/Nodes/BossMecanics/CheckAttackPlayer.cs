using UnityEngine;

public class CheckAttackPlayer : DecoratorNode
{
    private Transform PlayerTransform;
    private Transform BossTransform;
    public float PowerAttackPlayer = 0.1f;


    protected override void OnStart(BehaviourTreeLauncher launcher)
    {
        BossTransform = launcher.transform;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (PlayerTransform == null || BossTransform == null)
        {
            return State.Failure;
        }

        /*        
            float bossCurrentHealth = launcher.currentHealth;
            float bossMaxHealth = launcher.maxHealth;

            float powerfullAttack = BossTransform * PowerAttackPlayer;

            if ((bossCurrentHealth * 0.1f) <= powerfullAttack))
        */

        if (PlayerTransform != null || BossTransform != null)
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
