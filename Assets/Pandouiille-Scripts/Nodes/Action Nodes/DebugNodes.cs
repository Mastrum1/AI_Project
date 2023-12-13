using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class DebugNodes : ActionNode
    {
        public string message;

        #region Constructors
        protected override void OnStart() => Debug.Log(message);

        protected override State OnUpdate() 
        {
            Debug.Log($"OnUpdate: {message}");
            return State.Success;
        }

        protected override void OnStop() => Debug.Log(message);

        #endregion
    }
}
