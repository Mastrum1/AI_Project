using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;

namespace ViewBehaviorTree.Editor
{
    public class BehaviorTreeEditor : EditorWindow
    {
        private TreeView treeView;
        private IMGUIContainer treeContainer;

        private UnityEditor.Editor treeEditor;


        [MenuItem("Window/Behavior Tree/Editor")]
        public static void OpenTreeEditor()
        {
            GetWindow<BehaviorTreeEditor>("Behavior Tree Editor");
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            if (Selection.activeObject is BehaviorTree)
            {
                OpenTreeEditor();
                return true;
            }
            return false;
        }

        private void CreateGUI()
        {
            VisualTreeAsset vt = Resources.Load<VisualTreeAsset>("BehaviorTreeEditor");
            vt.CloneTree(rootVisualElement);


        
        }

        private void OnGUI()
        {
            GUILayout.Label("This is a label", EditorStyles.boldLabel);
        }
    }

}