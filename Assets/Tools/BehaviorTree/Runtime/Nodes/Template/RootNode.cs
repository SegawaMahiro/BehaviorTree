using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorTree
{
    public class RootNode : BehaviorTreeNode
    {
        protected override NodeState OnUpdate() {
            foreach (var child in Children) {
                switch (child.Update()) {
                    case NodeState.Running:
                        return NodeState.Running;
                    case NodeState.Failure:
                        return NodeState.Failure;

                    case NodeState.Success:
                        break;
                }
                return NodeState.Success;
            }
            return NodeState.Failure;
        }
    }
}
