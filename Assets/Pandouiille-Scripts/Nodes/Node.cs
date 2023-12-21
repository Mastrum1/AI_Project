using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Node : ScriptableObject
{
    public enum State
    {
        Running,
        Success,
        Failure
    }

     [HideInInspector] public State state = State.Running;
     [HideInInspector] public bool isStarted = false;
     public string guid;
     public Vector2 position;

    public State MyUpdate(BehaviourTreeLauncher launcher)
    {
        if(!isStarted)
        {
            OnStart(launcher);
            isStarted = true;
        }

        state = OnUpdate(launcher);
        if(state == State.Success || state == State.Failure)
        {
            OnStop(launcher);
            isStarted = false;
        }

        return state;
    }

    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    protected abstract void OnStart(BehaviourTreeLauncher launcher);
    protected abstract void OnStop(BehaviourTreeLauncher launcher);
    protected abstract State OnUpdate(BehaviourTreeLauncher launcher);
}
