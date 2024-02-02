using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class CompareValues : CompositeNode
    {
        enum Operator
        {
            Equal, // ==
            NotEqual, // !=
            LessThan, // <
            GreaterThan, // >
            LessThanOrEqual, // <=
            GreaterThanOrEqual // =>
        }
        [SerializeReference, SubclassSelector] BlackBoardVariable _leftVariable;
        [SerializeField] Operator _operator;
        [SerializeReference, SubclassSelector] BlackBoardVariable _rightVariable;

        protected override NodeState OnUpdate() {
            if (TryCompareValue()) {
                return Children[0].Update();
            }
            return Children[1].Update();
        }
        private (object, object) GetVariables() {
            object value1 = GetValue(_leftVariable);
            object value2 = GetValue(_rightVariable);
            return (value1, value2);
        }
        private object GetValue(BlackBoardVariable variable) {
            if (variable.Key != "") {
                return RootTree.BlackBoard.GetValue(variable.Key);
            }
            else {
                return variable.Value;
            }
        }
        private bool TryCompareValue() {
            (object value1, object value2) = GetVariables();
            switch (_operator) {
                case Operator.Equal:
                    return CompareEqual(value1, value2);
                case Operator.NotEqual:
                    return CompareNotEqual(value1, value2);
                case Operator.LessThan:
                    return CompareLessThan(value1, value2);
                case Operator.GreaterThan:
                    return CompareGreaterThan(value1, value2);
                case Operator.LessThanOrEqual:
                    return CompareLessThanOrEqual(value1, value2);
                case Operator.GreaterThanOrEqual:
                    return CompareGreaterThanOrEqual(value1, value2);
                default:
                    return false;
            }
        }
        private bool CompareEqual(object value1, object value2) {
            // floatに変換可能な場合
            if (value1 is float || value2 is float) {
                // float型で比較を行う
                return Convert.ToSingle(value1) == Convert.ToSingle(value2);
            }
            return value1.Equals(value2);
        }

        private bool CompareNotEqual(object value1, object value2) {
            if (value1 is float || value2 is float) {
                return Convert.ToSingle(value1) != Convert.ToSingle(value2);
            }
            return !value1.Equals(value2);
        }
        private bool CompareLessThan(object value1, object value2) {
            return Convert.ToSingle(value1) < Convert.ToSingle(value2);
        }

        private bool CompareGreaterThan(object value1, object value2) {
            return Convert.ToSingle(value1) > Convert.ToSingle(value2);
        }

        private bool CompareLessThanOrEqual(object value1, object value2) {
            return Convert.ToSingle(value1) <= Convert.ToSingle(value2);
        }

        private bool CompareGreaterThanOrEqual(object value1, object value2) {
            return Convert.ToSingle(value1) >= Convert.ToSingle(value2);
        }
    }
}
