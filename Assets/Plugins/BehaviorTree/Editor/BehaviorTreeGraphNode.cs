﻿using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;

namespace BehaviorTreeEditor
{
    public class BehaviorTreeGraphNode : Node
    {
        private BehaviorTreeNode _node;
        private string _guid;

        private Toggle _breakpointToggle;

        public BehaviorTreeNode Node { get { return _node; } }
        public string GUID { get { return _guid; } }

        //"Assets/Plugins/BehaviorTree/Resources/BehaviorTreeNode.uxml"
        public BehaviorTreeGraphNode(BehaviorTreeNode node) : base("Assets/Plugins/BehaviorTree/Resources/BehaviorTreeNode.uxml") {
            _node = node;
            _guid = node.guid;
            title = node.GetType().Name;

            style.left = node.rect.position.x;
            style.top = node.rect.position.y;

            _breakpointToggle = this.Q<Toggle>("Breakpoint");
            _breakpointToggle.RegisterValueChangedCallback(e => OnBreakpointToggleValueChanged(e.newValue));

            switch (node) {
                case RootNode:
                    capabilities = Capabilities.Deletable;
                    CreateOutputPort(Port.Capacity.Single);
                    break;
                case LeafNode:
                    CreateInputPort();
                    break;
                case DecoratorNode:
                    CreateInputPort();
                    CreateOutputPort(Port.Capacity.Single);
                    break;
                case BranchNode:
                    CreateInputPort();
                    CreateOutputPort(Port.Capacity.Multi);
                    break;
                default:
                    Debug.LogError("存在しないnodeの型です");
                    break;
            }
        }
        private void CreateInputPort() {

            var inputPort = Port.Create<Edge>(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(Port));
            inputPort.portName = "";
            inputPort.style.flexDirection = FlexDirection.Column;


            inputContainer.Add(inputPort);
        }
        private void CreateOutputPort(Port.Capacity capacity) {
            var outputPort = Port.Create<Edge>(Orientation.Vertical, Direction.Output, capacity, typeof(Port));
            outputPort.portName = "";
            outputPort.style.flexDirection = FlexDirection.ColumnReverse;
            outputContainer.Add(outputPort);
        }
        private void OnBreakpointToggleValueChanged(bool value) {
            _node.breakpoint = value;
        }
    }
}