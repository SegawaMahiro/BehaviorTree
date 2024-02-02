using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Parallel : BranchNode
    {
        protected override NodeState OnUpdate() {
            bool isAnySuccess = true;
            bool isAnyRunning = false;

            foreach (var child in Children) {
                switch (child.Update()) {
                    case NodeState.Success:
                        break;
                    case NodeState.Failure:
                        return NodeState.Failure;
                    case NodeState.Running:
                        isAnyRunning = true;
                        break;
                }
            }
            if (isAnySuccess) {
                return NodeState.Success;
            }
            if (isAnyRunning) {
                return NodeState.Running;
            }
            return NodeState.Failure;
        }
    }
}
