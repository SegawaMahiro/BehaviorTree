using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    internal class TriggerAnimation : LeafNode
    {
        private Animator _animator;
        [SerializeField] string _triggerName;
        protected override void OnAwake() {
            _animator = RootTree.gameObject.GetComponent<Animator>();
        }

        protected override NodeState OnUpdate() {
            _animator.SetTrigger(_triggerName);
            return NodeState.Success;
        }
    }
}
