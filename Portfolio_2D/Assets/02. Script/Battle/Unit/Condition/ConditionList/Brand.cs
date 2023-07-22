using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 지속형 상태이상인 낙인의 상태이상 클래스
 */

namespace Portfolio.condition
{
    public class Brand : ContinuationCondition
    {

        public Brand(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            // 방어력 감소
            unit.DefencePoint *= 0.8f;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            // 방어력 복구
            unit.DefencePoint /= 0.8f;
        }
    }
}