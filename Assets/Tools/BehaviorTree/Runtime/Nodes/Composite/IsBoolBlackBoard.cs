using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class IsBoolBlackBoard : CompositeNode
    {
        [SerializeField] string _key;
        protected override NodeState OnUpdate() {
            var value = RootTree.BlackBoard.GetValue(_key);
            if (value is bool) {
                bool flag = (bool)value;
                if (flag) {
                    return Children[0].Update();
                }
                return Children[1].Update();
            }
            return NodeState.Failure;
        }
    }
}
