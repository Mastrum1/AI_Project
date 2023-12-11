using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    public delegate NodeStates NodeReturn();

    protected NodeStates _nodeState;

    public NodeStates nodeState
    {
        get { return _nodeState; }
    }
    protected Node() {}

    public abstract NodeStates Evaluate();
}
