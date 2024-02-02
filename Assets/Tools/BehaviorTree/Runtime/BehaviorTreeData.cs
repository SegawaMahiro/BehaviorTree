using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviorTree
{
    public class BehaviorTreeData : MonoBehaviour
    {
        #region Field
        [Header("_______________________________________________________")]
        // behaviortree内で使用できる変数
        [SerializeField] BehaviorTreeBlackBoard _blackBoard = new();

        [Header("_______________________________________________________")]
        [Header("現在選択中のノード詳細")]
        [SerializeReference] BehaviorTreeNode _selectingNode;
        [Header("_______________________________________________________")]

        // treeの実行状態
        [SerializeField] BehaviorTreeNode.NodeState _treeStatue = BehaviorTreeNode.NodeState.Running;

        [Header("このtreeに存在するすべてのノード")]
        [SerializeReference,ReadOnly] List<BehaviorTreeNode> _nodes = new List<BehaviorTreeNode>();

        // treeの実行を開始するnode
        [SerializeReference, ReadOnly] BehaviorTreeNode _root;
        #endregion

        #region Properties
        public BehaviorTreeBlackBoard BlackBoard { get { return _blackBoard; } }
        public BehaviorTreeNode Root { get { return _root; } }
        public BehaviorTreeNode.NodeState TreeStatue { get { return _treeStatue; } }
        public List<BehaviorTreeNode> Nodes { get { return _nodes; } }
        #endregion

        private void Update() {
            OnUpdate();
        }

        public BehaviorTreeNode.NodeState OnUpdate() {
            if (Root.State == BehaviorTreeNode.NodeState.Running) {
                _treeStatue = _root.Update();
            }
            return _treeStatue;
        }

        /// <summary>
        /// graphview内のnodeと同じ情報を持つnodeの作成
        /// </summary>
        /// <param name="type">作成するnodeの型</param>
        /// <param name="position">生成位置</param>
        /// <returns></returns>
        public BehaviorTreeNode CreateNode(System.Type type, Vector2 position) {
            // 派生クラスのインスタンスを生成
            BehaviorTreeNode node = Activator.CreateInstance(type) as BehaviorTreeNode;

            // 開始ノードは一つのみ存在可能
            if (type == typeof(RootNode)) {
                _root = node;
            }

            // Nodeの所有者をこのComponentに設定
            node.RootTree = this;
            // Nodeの名前をクラス名に設定
            node.Name = type.Name;
            // 固有IDの作成
            node.Guid = Guid.NewGuid().ToString();
            // Node生成時の位置を保存
            node.Rect.position = position;
            // Nodeを追加
            _nodes.Add(node);
            return node;
        }

        /// <summary>
        /// 消されたgraphnodeのguidと一致するnodeを削除
        /// </summary>
        /// <param name="guid">消去するnodeのguid</param>
        public void DeleteNode(string guid) {
            BehaviorTreeNode targetNode = _nodes.FirstOrDefault(node => node.Guid == guid);

            if (targetNode != null) {
                Nodes.Remove(targetNode);
            }
        }

        public void SetNodePosition(string guid, Vector2 position) {
            BehaviorTreeNode targetNode = _nodes.FirstOrDefault(node => node.Guid == guid);

            if (targetNode != null) {
                targetNode.Rect.position = position;
            }
        }
        public void SetSelectingNode(string guid) {
            BehaviorTreeNode targetNode = _nodes.FirstOrDefault(node => node.Guid == guid);
            if (targetNode != null) {
                _selectingNode = targetNode;
            }
        }
    }
}
