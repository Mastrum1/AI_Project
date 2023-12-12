using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ViewBehaviorTree
{
    [System.Serializable]
    public class WaitNode : ActionNode
    {
        public float waitTime = 1f;

        private float startTime;

        #region Constructors
        protected override void OnStart() => startTime = Time.time;

        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            return Time.time - startTime > waitTime ? State.Success : State.Running; 
        }
        #endregion
    }
}
