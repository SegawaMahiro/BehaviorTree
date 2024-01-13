using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomAttributes
{

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position) {
            GUI.enabled = false;
        }
        public override float GetHeight() {
            return 0f;
        }
    }
}