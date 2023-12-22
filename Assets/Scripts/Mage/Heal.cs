using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Heal : ActionNode
{
    [SerializeField] private GameObject _healingSpell;
    [SerializeField] private GameObject[] allies;
    
    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        for (int i = 0; i < allies.Length; i++)
        {
            if (allies[i].tag == "Assassin" && Vector3.Magnitude(allies[i].transform.position - launcher.transform.position) < 2)
            {
                allies[i].GetComponent<EnemyController>().Heal(2);
                Instantiate(_healingSpell);
                return State.Success;
            }
        }
        return State.Failure;
    }
}