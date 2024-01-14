using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class BehaviorTreeBlackBoard
    {
        private Dictionary<string, object> _variableDictionary;
        [SerializeReference, SubclassSelector]
        List<BlackBoardVariable> _blackBoard = new();

        /// <summary>
        /// Keyに設定されている値を取得
        /// </summary>
        /// <param name="key">blackboardに設定している変数の名前</param>
        /// <returns></returns>
        public object GetValue(string key) {
            // keyに要素が設定されていない場合すべての要素を紐づける
            if (_variableDictionary == null) {
                _variableDictionary = new Dictionary<string, object>();
                // inspectorに登録されているすべてのblackboard
                foreach (var variable in _blackBoard) {
                    _variableDictionary[variable.Key] = variable.Value;
                }
            }
            // keyが一致した場合その値を返す
            if (_variableDictionary.ContainsKey(key)) return _variableDictionary[key];
            return null;
        }
        public void SetValue(BlackBoardVariable variable) {
            var variableKey = variable.Key;
            var variableValue = variable.Value;

            if (!_variableDictionary.ContainsKey(variableKey)) {
                Debug.LogError("dictionary内に一致するkeyが見つかりません。");
                Debug.Break();
            }
            if (_variableDictionary[variableKey].GetType() == variableValue.GetType()) {
                _variableDictionary[variableKey] = variableValue;
            }
            else {
                Debug.LogError("keyの値と型が一致していため変更できません。");
                Debug.Break();
            }
        }
    }
}
