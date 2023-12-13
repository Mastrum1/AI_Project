using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BehaviorTree.Node;

namespace BehaviorTree
{
    public abstract class CompositeNode : Node, IParentNode, IChildNode
    {
        public List<Node> children = new List<Node>();

        public Node GetChildNode()
        {
            throw new System.NotImplementedException();
        }

        public List<Node> GetChildrenNodes()
        {
            throw new System.NotImplementedException();
        }
    }
}

