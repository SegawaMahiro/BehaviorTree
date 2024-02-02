using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class ToggleGameObject : LeafNode
    {
        [SerializeField] GameObject _targetObject;
        [SerializeField] bool _toggle;
        protected override NodeState OnUpdate() {
            _targetObject.SetActive(_toggle);
            return NodeState.Success;
        }
    }
}
