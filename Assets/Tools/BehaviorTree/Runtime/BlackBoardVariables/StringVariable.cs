using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class StringVariable : BlackBoardVariable
    {
        public StringVariable() {

        }
        [SerializeField] private string _key;
        [SerializeField] private string _value;
        public override string Key => _key;

        public override object Value => _value;
    }
}
