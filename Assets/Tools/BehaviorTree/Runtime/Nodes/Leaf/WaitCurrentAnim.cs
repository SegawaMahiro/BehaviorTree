using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class WaitCurrentAnim : LeafNode
    {
        private Animator _animator;
        private float _startTime;
        protected override void OnAwake() {
            _animator = RootTree.gameObject.GetComponent<Animator>();
        }
        protected override void OnStart() {
            _startTime = Time.time;
        }

        protected override NodeState OnUpdate() {
            var animLength = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            if (Time.time - _startTime > animLength) {
                return NodeState.Success;
            }
            return NodeState.Running;
        }
    }
}
