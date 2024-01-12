using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorTree
{
    public class RootNode : BehaviorTreeNode
    {
        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override Status OnUpdate() {
            foreach (var child in children) {
                switch (child.Update()) {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        return Status.Failure;

                    case Status.Success:
                        break;
                }
                return Status.Success;
            }
            return Status.Failure;
        }
    }
}
