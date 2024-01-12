using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{

    public abstract class BehaviorTreeNode : ScriptableObject
    {
        public enum Status
        {
            Success, // 実行成功
            Failure, // 実行失敗
            Running // 実行中。次回にRunningを返したノードが再度呼ばれる
        }

        // デフォルトのnodeの大きさ
        private const float NodeWidth = 160f;
        private const float NodeHeight = 50f;

        // nodeごとのid
        [HideInInspector] public string guid;

        // 表示するnodeの大きさ
        [HideInInspector] public Rect rect = new Rect(0, 0, NodeWidth, NodeHeight);

        public Status status = Status.Running;

        public List<BehaviorTreeNode> children = new List<BehaviorTreeNode>();

        public bool breakpoint = false;

        private bool _started = false;


        public Status Update() {
            if (!_started) {
                OnStart();

                if (breakpoint) {
                    Debug.Break();
                }

                _started = true;
            }
            status = OnUpdate();
            if (status == Status.Failure || status == Status.Success) {
                OnStop();
                _started = false;
            }

            return status;
        }
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract Status OnUpdate();
        public BehaviorTreeNode Clone() {
            BehaviorTreeNode node = Instantiate(this);
            node.children = children?.ConvertAll(x => x.Clone());
            return node;
        }
    }

}

