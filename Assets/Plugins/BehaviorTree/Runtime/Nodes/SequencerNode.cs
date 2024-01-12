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

        protected override Status OnUpdate() {
            var child = children[_nodeCount];
            switch (child.Update()) {
                case Status.Running: 
                    return Status.Running;
                case Status.Failure:
                    return Status.Failure;

                case Status.Success:
                    _nodeCount++;
                    break;
            }
            return _nodeCount == children.Count ? Status.Success : Status.Running;
        }
    }
}
