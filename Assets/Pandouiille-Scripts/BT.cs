using Unity.Profiling;
using UnityEngine.Events;
using UnityEngine;


namespace BehaviorTree
{
    [DisallowMultipleComponent]
    public class MonoBehaviourTree : MonoBehaviour
    {

        private static ProfilerMarker s_TickMarker = new ProfilerMarker("BehaviorTree.Tick");

        [HideInInspector]
        public Node SelectEditorNode;
        public bool isFinished = false;
        public MonoBehaviourTree parent;

        public event UnityAction OnTick;
        
        internal float deltaTime;
        public float lastTick { get; private set; }

        // private Root ;  Root node of the tree

    }
}
