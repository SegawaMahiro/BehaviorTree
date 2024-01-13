using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using BehaviorTree;

namespace BehaviorTreeEditor
{
    [CustomEditor(typeof(BehaviorTreeData))]
    internal class BehaviorTreeInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            if (GUILayout.Button("OpenBehaviorTreeEditor")) {
                BehaviorTreeData runner = (BehaviorTreeData)target;
                BehaviorTreeGraphWindow window = ScriptableObject.CreateInstance<BehaviorTreeGraphWindow>();
                window.OnOpen(runner);
            }
            if (GUILayout.Button("Add BlackBoardVariable")) {
                BehaviorTreeData runner = (BehaviorTreeData)target;
                AddVariableWindow variableWindow = ScriptableObject.CreateInstance<AddVariableWindow>();
                variableWindow.OnOpen(AddVariable);
            }
        }
        private void AddVariable(string name, Type type) {
            Debug.Log($"Name: {name}, Type: {type}");
        }
    }
    internal class AddVariableWindow : EditorWindow
    {
        private Action<string, Type> addVariableCallback;
        private string variableName = "";
        private Type variableType = typeof(string);

        public void OnOpen(Action<string, Type> callback) {
            addVariableCallback = callback;
            ShowUtility();
        }

        private void OnGUI() {
            GUILayout.Label("Add BlackBoardVariable", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Name:");
            variableName = EditorGUILayout.TextField(variableName);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Type:");
            Type variableType = EditorGUILayout.TextField("").GetType();
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Add Variable")) {
                addVariableCallback?.Invoke(variableName, variableType);
                Close();
            }
        }
    }
}
