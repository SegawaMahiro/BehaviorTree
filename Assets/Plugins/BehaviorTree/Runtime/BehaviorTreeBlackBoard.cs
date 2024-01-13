using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    [System.Serializable]
    internal class BehaviorTreeBlackBoard
    {
       [SerializeReference] List<BlackBoardVariable> variables = new List<BlackBoardVariable>();

    }
}
