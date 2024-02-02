using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class BehaviorTreeBlackBoard
    {
        [SerializeReference, SubclassSelector]
        List<BlackBoardVariable> _blackBoard = new List<BlackBoardVariable>();

        /// <summary>
        /// Keyに設定されている値を取得
        /// </summary>
        /// <param name="key">blackboardに設定している変数の名前</param>
        /// <returns></returns>
        public object GetValue(string key) {
            // keyが一致した場合その値を返す
            foreach (var variable in _blackBoard) {
                if (variable.Key == key) {
                    return variable.Value;
                }
            }

            return null;
        }

        public void SetValue(BlackBoardVariable variable) {
            var variableKey = variable.Key;
            var variableValue = variable.Value;

            // keyに要素が設定されていない場合すべての要素を紐づける
            if (_blackBoard == null) {
                Debug.LogError("blackBoardが初期化されていません。");
                return;
            }

            // keyが一致した場合その値を更新
            for (int i = 0; i < _blackBoard.Count; i++) {
                if (_blackBoard[i].Key == variableKey) {
                    if (_blackBoard[i].Value.GetType() == variableValue.GetType()) {
                        _blackBoard[i] = variable;
                        return;
                    }
                    else {
                        Debug.LogError("keyの値と型が一致していないため変更できません。");
                        Debug.Break();
                        return;
                    }
                }
            }

            Debug.LogError("dictionary内に一致するkeyが見つかりません。");
            Debug.Break();
        }
    }
}
