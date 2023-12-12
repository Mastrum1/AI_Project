using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewBehaviorTree;

namespace ViewBehaviorTree
{
    public abstract class OverrideNode : DecoratorNode
    {
        #region Constructors
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            childNode.Update();
            return State.Running;
        }

        #endregion
    }
}