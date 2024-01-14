using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class LayerMaskVariable : BlackBoardVariable
    {
        public LayerMaskVariable() {

        }
        [SerializeField] private string _key;
        [SerializeField] private LayerMask _value;
        public override string Key => _key;

        public override object Value => _value;
    }
}
