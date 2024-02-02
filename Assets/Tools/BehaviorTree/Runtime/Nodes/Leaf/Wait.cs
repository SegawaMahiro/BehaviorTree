using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Wait : LeafNode
    {
        [SerializeField] float _duration = 1f;
        private float _startTime;
        protected override void OnStart() {
            _startTime = Time.time;
        }
        protected override NodeState OnUpdate() {
            if (Time.time - _startTime > _duration) {
                return NodeState.Success;
            }
            return NodeState.Running;
        }
    }
}
