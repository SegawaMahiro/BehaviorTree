using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Repeater : DecoratorNode
    {
        protected override NodeState OnUpdate() {
            foreach (var child in Children) {
                child.Update();
                return NodeState.Running;
            }
            return NodeState.Failure;
        }
    }
}
