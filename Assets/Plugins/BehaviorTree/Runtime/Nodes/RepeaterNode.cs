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

        protected override NodeState OnUpdate() {
            foreach (var child in Children) {
                child.Update();
                return NodeState.Running;
            }
            return NodeState.Failure;
        }
    }
}
