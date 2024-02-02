using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class BoolVariable : BlackBoardVariable
    {
        public BoolVariable() {

        }
        [SerializeField] private string _key;
        [SerializeField] private bool _value;
        public override string Key => _key;

        public override object Value => _value;
    }
}
