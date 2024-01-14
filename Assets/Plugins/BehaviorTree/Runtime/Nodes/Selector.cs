using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : BranchNode
    {
        private int _nodeCount;
        protected override void OnStart() {
            _nodeCount = 0;
        }

        protected override void OnStop() {
        }

        protected override NodeState OnUpdate() {
            while (_nodeCount < Children.Count) {
                var child = Children[_nodeCount];
                switch (child.Update()) {
                    case NodeState.Success:
                        return NodeState.Success;
                    case NodeState.Failure:
                        _nodeCount++;
                        break;
                    case NodeState.Running:
                        return NodeState.Running;
                }
            }

            return NodeState.Failure;
        }
    }
}
