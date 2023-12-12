using UnityEngine;


namespace ViewBehaviorTree
{
    [CreateAssetMenu(fileName = "New Behavior Tree", menuName = "Behavior Tree")]
    public class BehaviorTree : ScriptableObject
    {

        public Node rootNode;

        public Node.State treeState = Node.State.Running;

        private bool m_hasRootNode;

        public Node.State Update()
        {
            if (!m_hasRootNode)
            {
                m_hasRootNode = rootNode != null;
                if (!m_hasRootNode)
                {
                    Debug.LogError($"{name}needs a root node",this);
                }
            }

            if (m_hasRootNode)
            {
                treeState = rootNode.Update();
            }
            else
            {
                treeState = Node.State.Failure;
            }
            return treeState;
        }
    }
}
