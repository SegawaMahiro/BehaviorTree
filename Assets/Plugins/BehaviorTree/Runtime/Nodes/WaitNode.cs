using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class WaitNode : LeafNode
    {
        [SerializeField] float _duration = 1f;
      //  public float Duration { get { return _duration; } } 
        private float _startTime;
        protected override void OnStart() {
            _startTime = Time.time;
        }

        protected override void OnStop() {
        }

        protected override Status OnUpdate() {
            if (Time.time - _startTime > _duration) {
                return Status.Success;
            }
            return Status.Running;
        }
    }
}
