using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Node :ScriptableObject
{
    public enum State
    {
        Running,
        Success,
        Failure
    }

     public State state = State.Running;
     public bool isStarted = false;
     public string guid;
     public Vector2 position;

    public State Update()
    {
        if(!isStarted)
        {
            OnStart();
            isStarted = true;
        }

        state = OnUpdate();
        if(state == State.Success || state == State.Failure)
        {
            OnStop();
            isStarted = false;
        }

        return state;
    }

    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();
}
