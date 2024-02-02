using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class TargetDistance : CompositeNode
    {
        [SerializeField] GameObject _target;
        [SerializeField] float _distance;
        protected override NodeState OnUpdate() {
            if (Vector3.Distance(_target.transform.position, RootTree.gameObject.transform.position) < _distance) {
                return Children[0].Update();
            }
            return Children[1].Update();
        }
    }
}
