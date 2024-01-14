﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    internal class TriggerAnimation : LeafNode
    {
        [SerializeField] Animator _animator;
        [SerializeField] string _triggerName;
        protected override void OnStart() {        }

        protected override void OnStop() {
        }

        protected override NodeState OnUpdate() {
            _animator.SetTrigger(_triggerName);
            return NodeState.Success;
        }
    }
}
