using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class IntVariable : BlackBoardVariable
    {
        public IntVariable() {

        }
        [SerializeField] private string _key;
        [SerializeField] private int _value;
        public override string Key => _key;

        public override object Value => _value;
    }
}
