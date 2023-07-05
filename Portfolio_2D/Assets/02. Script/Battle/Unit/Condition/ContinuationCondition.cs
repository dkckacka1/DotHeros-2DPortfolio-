using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 지속형 상태이상의 추상 클래스
 */

namespace Portfolio.condition
{
    public abstract class ContinuationCondition : Condition
    {
        protected ContinuationCondition(ConditionData conditionData) : base(conditionData)
        {
        }

        // 상태이상이 해제될 때 호출할 순수 가상 함수
        public abstract void UnApplyCondition(BattleUnit unit);
    }

}