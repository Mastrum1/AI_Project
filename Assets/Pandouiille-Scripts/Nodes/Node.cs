using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [RequireComponent(typeof(MonoBehaviour))]
    public abstract class Node : MonoBehaviour
    {
        [HideInInspector]
        public Node parent;
        [HideInInspector]
        public List<Node> children = new List<Node>();
        [HideInInspector]
        public Rect rect = new Rect(0, 0, 100, 100);
        [HideInInspector]
        public MonoBehaviourTree behaviourTree;
        [System.NonSerialized]
        public Node.State state = Node.State.Running;



        public float LastTick => behaviourTree.lastTick;
        public float DeltaTime => Time.time - behaviourTree.deltaTime;


        public enum State
        {
            Success,
            Failure,
            Running
        }

        [SerializeField] private bool isRunning;

        public enum AbortType
        {
            None,
            Self,
            LowerPriority
        }
        
        public int nodeID;

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
        public abstract void AddChild(Node node);
        public abstract void RemoveChild(Node node);
        public abstract void RemoveAllChildren();

        public virtual Node GetParent()
        {
            return parent;
        }
        public virtual List<Node> GetChildren()
        {
            return children;
        }
        public bool IsChildOf(Node node)
        {
            if (parent == null)
            {
                return false;
            }
            if (parent == node)
            {
                return true;
            }
            return parent.IsChildOf(node);
        }
        public List<Node> GetFamily()
        {
            List<Node> family = new List<Node>();
            for (int i = 0; i < children.Count; i++)
            {
                family.Add(children[i]);
                family.AddRange(children[i].GetFamily());
            }
            return family;
        }
        public void SortChildren()
        {
            this.children.Sort((c, d) => c.rect.x.CompareTo(d.rect.x));
        }

        public virtual bool IsValid()
        {
            #if UNITY_EDITOR
            System.Reflection.FieldInfo[] propertyInfos = this.GetType().GetFields();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                if (propertyInfos[i].FieldType.IsSubclassOf(typeof(BaseVariableReference)))
                {
                    BaseVariableReference varReference = propertyInfos[i].GetValue(this) as BaseVariableReference;
                    if (varReference != null && varReference.isInvalid)
                    {
                        return false;
                    }
                }
            }
            #endif
            return true;
        }

        public abstract void Update();
        public abstract void Abort(Node node);
        public abstract void AbortLowerPriorityChildren(Node node);
        public abstract void Reset();



        public interface IChildNode
        {
            Node GetChildNode();
        }
        
        public interface IParentNode
        {
            List<Node> GetChildrenNodes();
        }

    }
}
