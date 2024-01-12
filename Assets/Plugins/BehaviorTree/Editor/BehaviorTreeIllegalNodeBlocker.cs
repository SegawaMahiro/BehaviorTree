//using BehaviorTree;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEditor;

//namespace BehaviorTreeEditor
//{

//    [CustomEditor(typeof(BehaviorTreeNode), true)]
//    public class BehaviorTreeNodeChildHider : Editor
//    {
//        SerializedProperty _children;

//        void OnEnable() {
//            _children = serializedObject.FindProperty("children");
//        }

//        public override void OnInspectorGUI() {
//            serializedObject.Update();

//            EditorGUILayout.LabelField("Children:");

//            for (int i = 0; i < _children.arraySize; i++) {
//                SerializedProperty child = _children.GetArrayElementAtIndex(i);
//                EditorGUILayout.PropertyField(child, true);
//            }

//            serializedObject.ApplyModifiedProperties();
//        }
//    }
//}