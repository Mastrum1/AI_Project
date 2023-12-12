using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViewBehaviorTree
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            Success,
            Failure,
            Running
        }
        [SerializeField] private State state = State.Running;

        [SerializeField] private bool isRunning;
        
        public int nodeID;

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();

        public State Update()
        {
            if (!isRunning)
            {
                OnStart();
            }

            state = OnUpdate();

            if (state != State.Running)
            {
                OnStop();
                isRunning = false;
            }
            else
            {
                isRunning = true;
            }

            return state;
        }

    }
}
