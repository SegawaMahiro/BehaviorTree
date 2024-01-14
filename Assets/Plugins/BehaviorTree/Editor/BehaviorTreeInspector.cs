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
        }
    }
}
