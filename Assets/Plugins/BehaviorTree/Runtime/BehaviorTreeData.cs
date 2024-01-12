using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu]
    public class BehaviorTreeData : ScriptableObject
    {
        [SerializeField] BehaviorTreeNode.Status _treeStatue = BehaviorTreeNode.Status.Running;

        [SerializeField] List<BehaviorTreeNode> _nodes = new List<BehaviorTreeNode>();

        [SerializeField] BehaviorTreeNode _root = null;

        [SerializeField] BehaviorTreeData _baseTree = null;
        public BehaviorTreeNode Root { get { return _root; } }
        public BehaviorTreeNode.Status TreeStatue { get { return _treeStatue; } }
        public List<BehaviorTreeNode> Nodes { get { return _nodes; } }



        public BehaviorTreeNode.Status Update() {
            if (Root.status == BehaviorTreeNode.Status.Running) {
                _treeStatue = Root.Update();
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
            BehaviorTreeNode node = CreateInstance(type) as BehaviorTreeNode;

            if (type == typeof(RootNode)) {
                _root = node;
            }

            node.name = type.Name;
            node.guid = Guid.NewGuid().ToString();
            node.rect.position = position;
            _nodes.Add(node);
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        /// <summary>
        /// 消されたgraphnodeのguidと一致するnodeを削除
        /// </summary>
        /// <param name="guid">消去するnodeのguid</param>
        public void DeleteNode(string guid) {
            BehaviorTreeNode targetNode = _nodes.FirstOrDefault(node => node.guid == guid);

            if (targetNode != null) {
                Nodes.Remove(targetNode);
                AssetDatabase.RemoveObjectFromAsset(targetNode);
                AssetDatabase.SaveAssets();
            }
        }

        public void SetNodePosition(string guid, Vector2 position) {
            BehaviorTreeNode targetNode = _nodes.FirstOrDefault(node => node.guid == guid);

            if (targetNode != null) {
                targetNode.rect.position = position;
                EditorUtility.SetDirty(targetNode);
                AssetDatabase.SaveAssets();
            }
        }
        public BehaviorTreeData Clone() {
            var origin = this;
            var tree = Instantiate(this);
            tree._baseTree = origin;
            tree._root = Root.Clone();
            return tree;
        }
        public void OverwriteInstancedTree() {
            _root = _baseTree.Root.Clone();
        }
    }
}
