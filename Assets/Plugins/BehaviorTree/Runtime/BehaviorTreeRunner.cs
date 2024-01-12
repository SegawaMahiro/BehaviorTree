using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTree
{
    public class BehaviorTreeRunner : MonoBehaviour
    {
        public BehaviorTreeData tree;


        private void Start() {
            tree = tree.Clone();
        }
        void Update () {
            tree.Root.Update();
        }
    }
}
