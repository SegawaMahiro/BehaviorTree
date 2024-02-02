using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class Vector2Variable : BlackBoardVariable
    {
        public Vector2Variable() {

        }
        [SerializeField] private string _key;
        [SerializeField] private Vector2 _value;
        public override string Key => _key;

        public override object Value => _value;
    }
}
