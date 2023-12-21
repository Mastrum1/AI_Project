using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Heal : ActionNode
{
    private GameObject ally;
    protected override State OnUpdate(BehaviourTreeLauncher launcher)
    {
        if (ally.tag == "Assassin" && Vector3.Magnitude(ally.transform.position - launcher.transform.position) < 2)
        {
            //ally.GetComponent<EnemyController>()._currentHp += 2;
            return State.Success;
        }
        else return State.Running;
    }
}