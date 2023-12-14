using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeNode : Node
{
    List<Node> children = new List<Node>();

    protected override void OnStart()
    {
        foreach (Node child in children)
        {
            //child.Start();
        }
    }
    protected override void OnStop()
    {
        foreach (Node child in children)
        {
           // child.Stop();
        }
    }

    protected override State OnUpdate()
    {
        foreach (Node child in children)
        {
            State state = child.Update();
            if (state != State.Success)
            {
                return state;
            }
        }
        return State.Success;
    }
}
