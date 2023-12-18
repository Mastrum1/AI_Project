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

    [HideInInspector] public State state = State.Running;
    [HideInInspector] public bool isStarted = false;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;

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

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();

}
