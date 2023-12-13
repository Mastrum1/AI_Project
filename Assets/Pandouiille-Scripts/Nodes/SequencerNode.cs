using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public class SequencerNode : CompositeNode
    {
        private int index = 0;

        #region Constructors
        protected override void OnStart() 
        {
            index = 0;
        }

        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            if (children is { Count: >= 1 })
            {
                return children[index]!.Update() switch
                {
                    State.Running => State.Running,
                    State.Success => index++ < children.Count - 1 ? State.Running : State.Success,
                    State.Failure => State.Failure,
                    _ => State.Failure
                };
            }
            Debug.LogWarning("No children in SequencerNode");
            return State.Failure;
        }

        #endregion
    }
}
