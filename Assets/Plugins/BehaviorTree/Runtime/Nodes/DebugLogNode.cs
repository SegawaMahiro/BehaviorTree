using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class DebugLogNode : LeafNode
    {
        [SerializeField] string _message;
      //  public string Message { get { return _message; } } 
        protected override void OnStart() {
        }
        protected override void OnStop() {
        }
        protected override Status OnUpdate() {
            Debug.Log($"OnUpdate : {_message}");
            return Status.Success;
        }
    }
}
