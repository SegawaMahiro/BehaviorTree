using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class SelectorNode : BranchNode
    {
        private int _nodeCount;
        protected override void OnStart() {
            _nodeCount = 0;
        }

        protected override void OnStop() {
        }

        protected override Status OnUpdate() {
            while (_nodeCount < children.Count) {
                var child = children[_nodeCount];
                switch (child.Update()) {
                    case Status.Success:
                        return Status.Success;
                    case Status.Failure:
                        _nodeCount++;
                        break;
                    case Status.Running:
                        return Status.Running;
                }
            }

            return Status.Failure;
        }
    }
}
