using BehaviorTree;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;


namespace BehaviorTreeEditor
{
    [InitializeOnLoad]
    public class WindowCloser
    {
        static WindowCloser() {
            EditorApplication.wantsToQuit += CloseWindows;
        }

        static bool CloseWindows() {
            EditorWindow[] windows = Resources.FindObjectsOfTypeAll<BehaviorTreeGraphWindow>();
            foreach (EditorWindow window in windows) {
                var view = window as BehaviorTreeGraphWindow;
                view.GraphView.SaveGraph();
                window.Close();
            }

            return true;
        }
    }
    public class BehaviorTreeGraphWindow : EditorWindow
    {
        private BehaviorTreeGraphView _graphView;
        private BehaviorTreeData _data;
        public BehaviorTreeGraphView GraphView { get { return _graphView; } }
        public BehaviorTreeData Data { get { return _data; } }

        public void Open(BehaviorTreeData data) {
            _data = data;
            _graphView = new BehaviorTreeGraphView(this);

            rootVisualElement.Add(_graphView);
            Show();
        }

        private void OnLostFocus() {
            _graphView.SaveGraph();
        }

        private void OnEnable() {
            CreateGraphView();
        }

        private void CreateGraphView() {
            _graphView = new BehaviorTreeGraphView(this);
            rootVisualElement.Add(_graphView);
        }

        [OnOpenAsset()]
      public static bool OnOpenAsset(int instanceId, int line) {
            if (EditorUtility.InstanceIDToObject(instanceId) is BehaviorTreeData) {

                var treeData = EditorUtility.InstanceIDToObject(instanceId) as BehaviorTreeData;
                
                if (HasOpenInstances<BehaviorTreeGraphWindow>()) {
                    var window = GetWindow<BehaviorTreeGraphWindow>(treeData.name, typeof(SceneView));

                    // データが空の場合新たなwindowの作成
                    if (window._data == null) {
                        window.Open(treeData);
                        return true;
                    }

                    // すでにlayoutに存在している場合そのwindowへ移動
                    if (window._data.GetInstanceID() == treeData.GetInstanceID()) {
                        window.Focus();
                        return false;
                    }
                    else {
                        // 存在していない場合新たに作成し移動
                        window.Open(treeData);
                        window.titleContent.text = treeData.name;
                        window.Focus();
                        return false;
                    }
                }
                else {
                    // windowそのものが存在しない場合新たに生成
                    var window = GetWindow<BehaviorTreeGraphWindow>(treeData.name, typeof(SceneView));

                    window.Open(treeData);
                    return true;
                }
            }

            return false;
        }
    }
}
