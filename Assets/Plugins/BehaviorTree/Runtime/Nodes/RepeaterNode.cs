using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class RepeaterNode : DecoratorNode
    {
        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override Status OnUpdate() {
            foreach (var child in children) {
                child.Update();
                return Status.Running;
            }
            return Status.Failure;
        }
    }
}
