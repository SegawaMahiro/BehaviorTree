using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class SequencerNode : BranchNode
    {
        private int _nodeCount;
        protected override void OnStart() {
            _nodeCount = 0;
        }

        protected override void OnStop() {
        }

        protected override NodeState OnUpdate() {
            var child = Children[_nodeCount];
            switch (child.Update()) {
                case NodeState.Running: 
                    return NodeState.Running;
                case NodeState.Failure:
                    return NodeState.Failure;

                case NodeState.Success:
                    _nodeCount++;
                    break;
            }
            return _nodeCount == Children.Count ? NodeState.Success : NodeState.Running;
        }
    }
}
