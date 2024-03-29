﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using BehaviorTree;
using UnityEditor.PackageManager.UI;
using UnityEngine.UIElements;

namespace BehaviorTreeEditor
{
    internal class BehaviorTreeNodeSearch : ScriptableObject, ISearchWindowProvider
    {
        private BehaviorTreeGraphView _graphView;
        private BehaviorTreeGraphWindow _window;

        public void Initialize(BehaviorTreeGraphView graphView,BehaviorTreeGraphWindow window) {
            _graphView = graphView;
            _window = window;
        }

        List<SearchTreeEntry> ISearchWindowProvider.CreateSearchTree(SearchWindowContext context) {
            var entries = new List<SearchTreeEntry>();
            entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (var type in assembly.GetTypes()) {

                    // rootnode以外のnodeを選択肢に追加
                    if (type.IsClass && !type.IsAbstract && (type.IsSubclassOf(typeof(BehaviorTreeNode)))
                        && type != typeof(RootNode)) {

                        entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 1, userData = type });
                    }
                }
            }

            return entries;
        }

        bool ISearchWindowProvider.OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context) {
            var type = entry.userData as System.Type;

            BehaviorTreeNode node = Activator.CreateInstance(type) as BehaviorTreeNode;
            var worldMousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent, context.screenMousePosition - _window.position.position);
            var localMousePosition = _graphView.contentViewContainer.WorldToLocal(worldMousePosition);

            node.Rect.position = localMousePosition;

            _graphView.CreateNodeView(_window.Data.CreateNode(node.GetType(),localMousePosition));

            return true;
        }
    }

}
