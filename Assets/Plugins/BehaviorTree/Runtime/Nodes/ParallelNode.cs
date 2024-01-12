using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class ParallelNode : BranchNode
    {
        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override Status OnUpdate() {
            bool isAnySuccess = true;
            bool isAnyRunning = false;

            foreach (var child in children) {
                switch (child.Update()) {
                    case Status.Success:
                        break;
                    case Status.Failure:
                        return Status.Failure;
                    case Status.Running:
                        isAnyRunning = true;
                        break;
                }
            }
            if (isAnySuccess) {
                return Status.Success;
            }
            if (isAnyRunning) {
                return Status.Running;
            }
            return Status.Failure;
        }
    }
}
