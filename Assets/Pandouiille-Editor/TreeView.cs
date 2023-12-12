using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Node = GraphViewBehaviorTree;

namespace GraphViewBehaviorTree.Editor.Views
{
    public class TreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<TreeView, GraphView.UxmlTraits> { }


        public Action<Node> onModeSelected;
    }
}
