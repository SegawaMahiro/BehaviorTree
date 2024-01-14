using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class DebugLog : LeafNode
    {
        [SerializeField] string _blackboardKey;
        protected override void OnStart() {
        }
        protected override void OnStop() {
        }
        protected override NodeState OnUpdate() {
            Debug.Log(RootTree.BlackBoard.GetValue(_blackboardKey));
            return NodeState.Success;
        }
    }
}
