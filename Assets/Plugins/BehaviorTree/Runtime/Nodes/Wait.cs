using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Wait : LeafNode
    {
        [SerializeField] float _duration = 1f;
      //  public float Duration { get { return _duration; } } 
        private float _startTime;
        protected override void OnStart() {
            _startTime = Time.time;
        }

        protected override void OnStop() {
        }

        protected override NodeState OnUpdate() {
            if (Time.time - _startTime > _duration) {
                return NodeState.Success;
            }
            return NodeState.Running;
        }
    }
}
