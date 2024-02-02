using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public abstract class BlackBoardVariable
    {
        public abstract string Key { get; }
        public abstract object Value { get; }

    }
}
