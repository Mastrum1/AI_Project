using UnityEngine;

public class CheckAttackPlayer : DecoratorNode
{
    public Transform playerTransform;
    public Transform bossTransform;
    public float PowerAttackPlayer = 0.1f;


    protected override void OnStart(BehaviourTreeLauncher launcher)
    {

    }

    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (playerTransform == null || bossTransform == null)
        {
            return State.Failure;
        }

/*        float bossCurrentHealth = bossTransform.GetComponent<EnemyHealth>().currentHealth;
        float bossMaxHealth = bossTransform.GetComponent<EnemyHealth>().maxHealth;
*/

        //float powerfullAttack = bossMaxHealth * PowerAttackPlayer;

        if (/*(bossCurrentHealth * 0.1f) <= powerfullAttack*/ 
            playerTransform != null || bossTransform != null)
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
