using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class SetBlackBoard : LeafNode
    {
        [SerializeReference, SubclassSelector] BlackBoardVariable _variable;
        protected override void OnStart() {
        }
        protected override void OnStop() {
        }
        protected override NodeState OnUpdate() {
            RootTree.BlackBoard.SetValue(_variable);
            return NodeState.Success;
        }
    }
}
