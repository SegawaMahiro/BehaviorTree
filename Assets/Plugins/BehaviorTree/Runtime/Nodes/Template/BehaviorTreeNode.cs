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

        // inspectorでの表示名
        public string Name;

        // このnodeを管理しているtree
        [SerializeField,ReadOnly] BehaviorTreeData _rootTree;

        // nodeごとのid
        [HideInInspector] public string Guid;

        // 表示するnodeの大きさ
        [HideInInspector] public Rect Rect = new Rect(0, 0, NodeWidth, NodeHeight);

        // nodeの実行状態
        public NodeState State = NodeState.Running;
        
        // 現在のnodeが完了時次に実行するnodeのリスト
        [SerializeReference, ReadOnly] public List<BehaviorTreeNode> Children = new();

        // このnodeが実行された時editorを中断する
        public bool Breakpoint = false;

        // 最初の実行の場合の動作フラグ
        private bool _isFirstSimulate = true;

        #region Properties
        public BehaviorTreeData RootTree { get { return _rootTree; } set { _rootTree = value; } }
        #endregion


        /// <summary>
        /// treeが更新された際の処理
        /// </summary>
        /// <returns></returns>
        public NodeState Update() {
            // 初回実行のみ処理を行う
            if (_isFirstSimulate) {
                OnAwake();
                OnStart();
                _isFirstSimulate = false;
            }
            // breakpointがついている場合editorを中断
            if (Breakpoint) {
                Debug.Break();
            }
            // 現在のstateを更新
            State = OnUpdate();
            if (State == NodeState.Failure || State == NodeState.Success) {
                OnStop();
                _isFirstSimulate = true;
            }

            return State;
        }
        protected void OnAwake() {

        }
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract NodeState OnUpdate();
    }

}

