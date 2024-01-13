using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{

    [System.Serializable]
    public abstract class BehaviorTreeNode
    {
        public enum NodeState
        {
            Success, // 実行成功
            Failure, // 実行失敗
            Running // 実行中。次回にRunningを返したノードが再度呼ばれる
        }

        // デフォルトのnodeの大きさ
        private const float NodeWidth = 160f;
        private const float NodeHeight = 50f;

         public string Name;
        // nodeごとのid
        [HideInInspector] public string Guid;

        // 表示するnodeの大きさ
        [HideInInspector] public Rect Rect = new Rect(0, 0, NodeWidth, NodeHeight);

        public NodeState State = NodeState.Running;

        [SerializeReference] public List<BehaviorTreeNode> Children = new List<BehaviorTreeNode>();

        public bool Breakpoint = false;

        private bool _isFirstSimulate = true;


        public NodeState Update() {
            if (_isFirstSimulate) {
                OnStart();

                if (Breakpoint) {
                    Debug.Break();
                }

                _isFirstSimulate = false;
            }
            State = OnUpdate();
            if (State == NodeState.Failure || State == NodeState.Success) {
                OnStop();
                _isFirstSimulate = true;
            }

            return State;
        }
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract NodeState OnUpdate();
    }

}

