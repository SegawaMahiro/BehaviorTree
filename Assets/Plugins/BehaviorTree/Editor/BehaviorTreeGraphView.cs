using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using BehaviorTree;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.PackageManager.UI;

namespace BehaviorTreeEditor
{
    public class BehaviorTreeGraphView : GraphView
    {
        private BehaviorTreeGraphWindow _window;
        private readonly Vector2 _rootNodePosition = new Vector2(500, 250);
        public BehaviorTreeGraphView(BehaviorTreeGraphWindow window) : base() {

            graphViewChanged += OnGraphViewChanged;
            _window = window;

            Initialize();
            SelectNode();
        }
        /// <summary>
        /// windowの初期化
        /// </summary>
        private void Initialize() {

            style.flexGrow = 1;
            style.flexShrink = 1;

            this.StretchToParentSize();
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            styleSheets.Add(Resources.Load<StyleSheet>("BackGround"));
            Insert(0, new GridBackground());

            GenerateMinimap();
            GenerateBlackBoard();

            if (_window.Data == null || _window.Data.Root == null) {
                GenerateRootNode();
            }
            else {
                LoadGraph();
            }
        }
        /// <summary>
        /// 破棄できない親ノードを作成
        /// </summary>
        private void GenerateRootNode() {
            if (_window.Data == null) return;
            CreateNodeView(_window.Data.CreateNode(typeof(RootNode), _rootNodePosition));

        }
        /// <summary>
        /// graphviewのnode情報を保存
        /// </summary>
        public void SaveGraph() {
            Dictionary<string, BehaviorTreeNode> nodeDictionary = new Dictionary<string, BehaviorTreeNode>();

            // ノードをディクショナリに追加
            foreach (BehaviorTreeGraphNode targetNode in nodes) {
                BehaviorTreeNode currentNode = _window.Data.Nodes.FirstOrDefault(node => node.guid == targetNode.GUID);

                if (currentNode != null) {
                    nodeDictionary.Add(targetNode.GUID, currentNode);
                }
            }

            // 左側にあるものから順にlistに追加
            foreach (BehaviorTreeGraphNode targetNode in nodes) {
                if (nodeDictionary.TryGetValue(targetNode.GUID, out var currentNode)) {
                    currentNode.children.Clear();

                    // すべてのedge portを取得
                    var outputPort = targetNode.outputContainer.Children().OfType<Port>().FirstOrDefault();
                    if (outputPort != null) {
                        // 接続をソート
                        var sortedConnections = outputPort.connections.OrderBy(edge => edge.input.node.GetPosition().position.x);

                        foreach (Edge edge in sortedConnections) {
                            // edgeに接続されているすべてのnodeを追加
                            BehaviorTreeGraphNode connectedNode = edge.input.node as BehaviorTreeGraphNode;
                            if (connectedNode != null && nodeDictionary.TryGetValue(connectedNode.GUID, out var connectedBehaviorTreeNode)) {
                                currentNode.children.Add(connectedBehaviorTreeNode);
                            }
                        }
                    }

                    // ノードごとに位置を更新
                    currentNode.rect.position = targetNode.GetPosition().position;
                }
            }

            EditorUtility.SetDirty(_window.Data);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// windowを開いているデータを読み取ってgraphへ反映
        /// </summary>
        private void LoadGraph() {
            Dictionary<string, BehaviorTreeGraphNode> nodeDictionary = new Dictionary<string, BehaviorTreeGraphNode>();

            foreach (var nodeData in _window.Data.Nodes) {
                BehaviorTreeGraphNode nodeView = new BehaviorTreeGraphNode(nodeData);
                nodeDictionary.Add(nodeData.guid, nodeView);
                AddElement(nodeView);
            }

            // エッジの生成
            foreach (var nodeData in _window.Data.Nodes) {
                if (nodeData.children == null) {
                    nodeData.children = new List<BehaviorTreeNode>(); // 子ノードのリストを初期化
                    continue;
                }

                // 子ノードを取得しedgeを作成
                foreach (var childNodeData in nodeData.children) {
                    if (nodeDictionary.TryGetValue(childNodeData.guid, out var childNodeView)) {
                        var outputPort = nodeDictionary[nodeData.guid].outputContainer.Children().OfType<Port>().FirstOrDefault();
                        var inputPort = childNodeView.inputContainer.Children().OfType<Port>().FirstOrDefault();

                        // portが存在する場合接続
                        if (outputPort != null && inputPort != null) {
                            var edge = new Edge { output = outputPort, input = inputPort };
                            // portをedgeを結びつける
                            edge.output.Connect(edge);
                            edge.input.Connect(edge);
                            AddElement(edge);
                        }
                    }
                }
            }
        }





        /// <summary>
        /// ノードを生成するdialogueの生成
        /// </summary>
        private void SelectNode() {
            var searchWindowProvider = ScriptableObject.CreateInstance<BehaviorTreeNodeSearch>();
            searchWindowProvider.Initialize(this, _window);

            nodeCreationRequest += context => {
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
            };
        }

        /// <summary>
        /// graphview内に新しいnodeを追加する
        /// </summary>
        /// <param name="node">追加するnodeのtype</param>
        public void CreateNodeView(BehaviorTreeNode node) {
            BehaviorTreeGraphNode nodeView = new BehaviorTreeGraphNode(node);
            AddElement(nodeView);
        }
        public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter) {
            return ports.ToList();
        }
        private void GenerateMinimap() {
            var minimap = new MiniMap();
            minimap.anchored = true;
            minimap.SetPosition(new Rect(x: 10, y: 30, width: 200, height: 140));
            Add(minimap);
        }
        private void GenerateBlackBoard() {
            var blackboard = new Blackboard();
            blackboard.Add(new BlackboardSection { title = "BlackBoard" });
            Add(blackboard);
        }

        /// <summary>
        /// graphviewのnodeが操作された時の処理
        /// </summary>
        /// <param name="graphViewChange">発生時のイベント</param>
        /// <returns></returns>
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange) {
            // ノードが消された場合
            if (graphViewChange.elementsToRemove != null && graphViewChange.elementsToRemove.Count > 0) {
                foreach (var element in graphViewChange.elementsToRemove) {
                    if (element is BehaviorTreeGraphNode node) {
                        string nodeID = node.GUID;
                        _window.Data.DeleteNode(nodeID);
                    }
                }
            }
            return graphViewChange;
        }
    }
}